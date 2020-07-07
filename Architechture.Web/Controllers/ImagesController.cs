using Architecture.Web.Models;
using Architecture.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using System.Drawing;
using Amazon.S3.Model;
using System.Linq;

namespace ImageResizeWebApp.Controllers
{
    //[Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly AzureStorageConfig storageConfig = null;
        private readonly AWSBucketConfig bucketConfig = null;
        private static RegionEndpoint bucketRegion;

        public ImagesController(IOptions<AzureStorageConfig> config, IOptions<AWSBucketConfig> awsBucketConfig)
        {
            storageConfig = config.Value;
            bucketConfig = awsBucketConfig.Value;
            bucketRegion = RegionEndpoint.APSouth1;
        }
        public async Task<IActionResult> getThumb()
        {
            //return Json(thumbnails());

            var clientS3 = new AmazonS3Client(bucketConfig.AwsAccessKeyId, bucketConfig.AwsSecretAccessKey, bucketRegion);
            var list = new List<string>();

            try
            {
                AmazonS3Client s3Client = new AmazonS3Client(bucketConfig.AwsAccessKeyId, bucketConfig.AwsSecretAccessKey, bucketRegion);
                var lista = s3Client.ListObjectsAsync(bucketConfig.BucketName, $"Images").Result;

                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketConfig.BucketName,// + @"/Images",
                    Prefix = "Images",
                    MaxKeys = 10
                };
                ListObjectsV2Response response;
                do
                {
                    response = await clientS3.ListObjectsV2Async(request);

                    // Process the response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        list.Add("https://magnusminds.s3.ap-south-1.amazonaws.com/" + entry.Key);
                        Console.WriteLine("key = {0} size = {1}",
                            entry.Key, entry.Size);
                    }
                    Console.WriteLine("Next Continuation Token: {0}", response.NextContinuationToken);
                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                Console.WriteLine("S3 error occurred. Exception: " + amazonS3Exception.ToString());
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                Console.ReadKey();
            }
            return Json(list);

        }



        public async Task<IActionResult> Index()
        {
            var list = new List<ImageList>();

            try
            {
                AmazonS3Client s3Client = new AmazonS3Client(bucketConfig.AwsAccessKeyId, bucketConfig.AwsSecretAccessKey, bucketRegion);
                // return First 1000 Files Info...
                var ListOf1000 = (s3Client.ListObjectsAsync(bucketConfig.BucketName, $"Images").Result)
                    .S3Objects.Select(s => new ImageList()
                    {
                        FileName = s.Key,
                        Url = "https://magnusminds.s3.ap-south-1.amazonaws.com/" + s.Key,
                        Size = s.Size.ToString()
                    });

                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = bucketConfig.BucketName,// + @"/Images",
                    Prefix = "Images",
                    MaxKeys = 10
                };
                ListObjectsV2Response response;
                do
                {
                    response = await s3Client.ListObjectsV2Async(request);

                    // Process the response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        list.Add(new ImageList()
                        {
                            FileName = entry.Key,
                            Url = "https://magnusminds.s3.ap-south-1.amazonaws.com/" + entry.Key,
                            Size = entry.Size.ToString()
                        });
                        Console.WriteLine("key = {0} size = {1}",
                            entry.Key, entry.Size);
                    }
                    Console.WriteLine("Next Continuation Token: {0}", response.NextContinuationToken);
                    request.ContinuationToken = response.NextContinuationToken;
                } while (response.IsTruncated);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                Console.WriteLine("S3 error occurred. Exception: " + amazonS3Exception.ToString());
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                Console.ReadKey();
            }
            return View(list);

        }



        // upload Image in AWS
        //// POST /images/upload
        [HttpPost("images/upload1")]
        //[HttpPost("[action]")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            try
            {

                List<UploadPartResponse> uploadResponses = new List<UploadPartResponse>();

                // Setup information required to initiate the multipart upload.
                InitiateMultipartUploadRequest initiateRequest = new InitiateMultipartUploadRequest
                {
                    BucketName = bucketConfig.BucketName,
                    Key = bucketConfig.AwsSecretAccessKey,
                };
                var clientS3 = new AmazonS3Client(bucketConfig.AwsAccessKeyId, bucketConfig.AwsSecretAccessKey, bucketRegion);

                // Initiate the upload.
                InitiateMultipartUploadResponse initResponse =
                    await clientS3.InitiateMultipartUploadAsync(initiateRequest);

                foreach (var formFile in files)
                {
                    if (StorageHelper.IsImage(formFile))
                    {
                        if (formFile.Length > 0)
                        {
                            var fileTransferUtility = new TransferUtility(clientS3);
                            using (Stream stream = formFile.OpenReadStream())
                            {
                              //// Option 4. Specify advanced settings.
                                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                                {
                                    BucketName = bucketConfig.BucketName + @"/Images",
                                    InputStream = stream,
                                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                                    //PartSize = 6291456, // 6 MB.
                                    Key = formFile.FileName,
                                    CannedACL = S3CannedACL.PublicRead
                                };
                                //fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
                                Console.WriteLine("Upload 4 completed");
                                
                                //// Upload a part and add the response to our list.

                                ////long partSize = 5 * (long)Math.Pow(2, 20); // 5 MB
                                ////Console.WriteLine("Uploading parts");
                                ////long filePosition = 0;
                                ////for (int i = 1; filePosition < formFile.Length; i++)
                                ////{
                                ////    UploadPartRequest uploadRequest = new UploadPartRequest
                                ////    {
                                ////        BucketName = bucketConfig.BucketName,
                                ////        Key = formFile.FileName,
                                ////        UploadId = initResponse.UploadId,
                                ////        PartNumber = i,
                                ////        PartSize = partSize,
                                ////        FilePosition = filePosition,
                                ////        InputStream = stream
                                ////    };

                                ////    //// Track upload progress.
                                ////    //uploadRequest.StreamTransferProgress +=
                                ////    //    new EventHandler<StreamTransferProgressArgs>(UploadPartProgressEventCallback);

                                ////    // Upload a part and add the response to our list.
                                ////    uploadResponses.Add(await clientS3.UploadPartAsync(uploadRequest));
                                ////    filePosition += partSize;
                                ////}

                                ////// Setup to complete the upload.
                                ////CompleteMultipartUploadRequest completeRequest = new CompleteMultipartUploadRequest
                                ////{
                                ////    BucketName = bucketConfig.BucketName,
                                ////    Key = bucketConfig.AwsSecretAccessKey,
                                ////    UploadId = initResponse.UploadId
                                ////};
                                ////completeRequest.AddPartETags(uploadResponses);

                                ////// Complete the upload.
                                ////CompleteMultipartUploadResponse completeUploadResponse = await clientS3.CompleteMultipartUploadAsync(completeRequest);
                            }
                        }
                        else
                        {
                            return new UnsupportedMediaTypeResult();
                        }
                    }
                }
                return new AcceptedResult();

            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", ex.Message);
                return BadRequest("Look like the image couldnt upload to the storage");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", ex.Message);
                return BadRequest(ex.Message);
            }

        }

        // upload Image in AZURE
        //// POST /images/upload
        [HttpPost("images/upload")]
        //[HttpPost("[action]")]
        public async Task<IActionResult> Upload1(ICollection<IFormFile> files)
        {
            bool isUploaded = false;

            try
            {
                if (files.Count == 0)
                    return BadRequest("No files received from the upload");

                if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                    return BadRequest("sorry, can't retrieve your azure storage details from appsettings.js, make sure that you add azure storage details there");

                if (storageConfig.ImageContainer == string.Empty)
                    return BadRequest("Please provide a name for your image container in the azure blob storage");

                foreach (var formFile in files)
                {
                    if (StorageHelper.IsImage(formFile))
                    {
                        if (formFile.Length > 0)
                        {
                            using (Stream stream = formFile.OpenReadStream())
                            {
                                isUploaded = await StorageHelper.UploadFileToStorage(stream, formFile.FileName, storageConfig);

                                if (isUploaded)
                                {
                                    var thum = GetReducedImage(50, 50, stream);
                                }
                            }
                        }
                    }
                    else
                    {
                        return new UnsupportedMediaTypeResult();
                    }
                }

                if (isUploaded)
                {
                    if (storageConfig.ThumbnailContainer != string.Empty)
                        return new AcceptedAtActionResult("GetThumbNails", "Images", null, null);
                    else
                        return new AcceptedResult();
                }
                else
                    return BadRequest("Look like the image couldnt upload to the storage");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // GET /api/images/thumbnails
        [HttpGet]
        //[HttpGet("thumbnails")]
        public async Task<IActionResult> thumbnails()
        {
            try
            {
                if (storageConfig.AccountKey == string.Empty || storageConfig.AccountName == string.Empty)
                    return BadRequest("Sorry, can't retrieve your Azure storage details from appsettings.js, make sure that you add Azure storage details there.");

                if (storageConfig.ImageContainer == string.Empty)
                    return BadRequest("Please provide a name for your image container in Azure blob storage.");

                List<string> thumbnailUrls = await StorageHelper.GetThumbNailUrls(storageConfig);
                return new ObjectResult(thumbnailUrls);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
using Amazon;

namespace Architecture.Web.Models
{
    public class AWSBucketConfig
    {
        public string AwsAccessKeyId { get; set; }
        public string AwsSecretAccessKey { get; set; }
        public RegionEndpoint RegionEndpoint { get; set; }
        public string BucketName { get; set; }
        public string BucketARN { get; set; }
    }
}

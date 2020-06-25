using System.Collections.Generic;

namespace Architecture.Utility
{
    public interface IResponse
    {
        IEnumerable<Error> Errors { get; set; }

        bool DidError { get; set; }
    }

    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Model { get; set; }
    }

    public interface IListResponse<TModel> : IResponse
    {
        IEnumerable<TModel> Model { get; set; }
    }

    public interface IPagedResponse<TModel> : IListResponse<TModel>
    {
        int ItemsCount { get; set; }

        double PageCount { get; }
    }

    public class Response : IResponse
    {
        public IEnumerable<Error> Errors { get; set; }

        public bool DidError { get; set; }
    }

    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public bool DidError { get; set; }

        public IEnumerable<Error> Errors { get; set; }

        public TModel Model { get; set; }
    }

    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }

        public bool DidError { get; set; }

        public IEnumerable<Error> Errors { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public int ItemsCount { get; set; }
    }

    public class PagedResponse<TModel> : IPagedResponse<TModel>
    {
        public bool DidError { get; set; }

        public IEnumerable<Error> Errors { get; set; }

        public IEnumerable<TModel> Model { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int ItemsCount { get; set; }

        public double PageCount => ItemsCount < PageSize ? 1 : (int)(((double)ItemsCount / PageSize) + 1);
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public string Message { get; set; }
    }
}
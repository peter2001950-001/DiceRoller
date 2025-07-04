﻿using System.Linq.Dynamic.Core;
using DiceRoller.Domain.Abstractions.Pagination;

namespace DiceRoller.Infrastructure.Utilities
{
    public static class PaginationExtensions
    {
        public static IEnumerable<T> Paged<T>(this IEnumerable<T> input, PaginationOptions? request)
        {
            if (request == null)
                return input;

            var output = input;
            if (request.StartAt > 0)
                output = output.Skip(request.StartAt);
            if (request.Count > 0)
                output = output.Take(request.Count);

            return output;
        }

        public static IQueryable<T> Paged<T>(this IQueryable<T> input, PaginationOptions? request)
        {
            if (request == null)
                return input;

            var output = input;
            if (request.StartAt > 0)
                output = output.Skip(request.StartAt);
            if (request.Count > 0)
                output = output.Take(request.Count);

            return output;
        }

        private static string ExtractSortFields(PaginationOptions? request)
        {
            if (request?.SortFields == null)
                return string.Empty;

            return string.Join(", ", request.SortFields.Where(f => !string.IsNullOrWhiteSpace(f)).Select(sortField =>
            {
                var fieldName = sortField;
                var sortDirection = " asc";
                if (sortField.StartsWith("+"))
                    fieldName = sortField.Substring(1);
                if (sortField.StartsWith("-"))
                {
                    fieldName = sortField.Substring(1);
                    sortDirection = " desc";
                }

                return fieldName + sortDirection;
            }));
        }

        public static IEnumerable<T> Sorted<T>(this IEnumerable<T> input, PaginationOptions? request)
        {
            if (request == null)
                return input;

            try
            {
                var orderFields = ExtractSortFields(request);
                return string.IsNullOrWhiteSpace(orderFields) ? input : input?.AsQueryable().OrderBy(orderFields);
            }
            catch
            {
                return input;
            }
        }

        public static IQueryable<T> Sorted<T>(this IQueryable<T> input, PaginationOptions? request)
        {
            if (request == null)
                return input;

            try
            {
                var orderFields = ExtractSortFields(request);
                return string.IsNullOrWhiteSpace(orderFields) ? input : input?.OrderBy(orderFields);
            }
            catch
            {
                return input;
            }
        }
    }
}

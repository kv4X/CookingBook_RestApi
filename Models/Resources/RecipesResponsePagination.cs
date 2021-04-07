using System;
using System.Collections.Generic;
using CookingBookApi.Models.Dtos;

namespace CookingBookApi.Models.Resources
{
    public class RecipesResponsePagination
    {
        public int CurrentPage { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }

        public IEnumerable<RecipeDto> Results { get; init;}
    }
}
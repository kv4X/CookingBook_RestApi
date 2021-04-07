namespace CookingBookApi.Models.Resources
{
    public class RecipeQueryParams
    {
        public int Limit { get; set; }
        public int Page { get; set; } = 1;
        public string SortBy { get; set; } = "";
        public string OrderBy { get; set; } = "";
    }
}
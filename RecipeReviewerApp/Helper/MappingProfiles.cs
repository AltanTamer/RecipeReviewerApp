using AutoMapper;
using RecipeReviewerApp.Dto;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();



        }
    }
}

﻿using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Models.Entities;

namespace MovieApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly MovieService movieService;
        public MoviesController(ApplicationDbContext dbContext, MovieService movieService)
        {
            this.dbContext = dbContext;
            this.movieService = movieService;
        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchMoviesFromApi()
        {
            try
            {
                var newMovies = await movieService.PobierzFilmyIZapisz();
                return Ok(newMovies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd pobierania filmów: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(dbContext.Movies.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = dbContext.Movies.Find(id);
            if(movie is null){
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieDto addMovieDto)
        {
            var movieEntity = new Movie()
            {
                Title = addMovieDto.Title,
                Director = addMovieDto.Director,
                Year = addMovieDto.Year
            };

            dbContext.Movies.Add(movieEntity);
            dbContext.SaveChanges();

            return Ok(movieEntity);

        }
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateMovie(int id, UpdateMovieDto updateMovieDto)
        {
            var movie = dbContext.Movies.Find(id);
            if(movie is null)
            {
                return NotFound();
            }

            movie.Title = updateMovieDto.Title;
            movie.Director = updateMovieDto.Director;
            movie.Year = updateMovieDto.Year;
            
            dbContext.SaveChanges();
            return Ok(movie);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = dbContext.Movies.Find(id);
            if(movie is null)
            {
                return NotFound();
            }
            
            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}

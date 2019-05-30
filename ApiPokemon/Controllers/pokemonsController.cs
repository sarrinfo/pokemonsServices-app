using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;
using ApiPokemon;

namespace ApiPokemon.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class pokemonsController : ApiController
    {
        private SARRINFODBPokemonEntities db = new SARRINFODBPokemonEntities();

        // GET: api/pokemons
        public IQueryable<pokemon> Getpokemon()
        {
            return db.pokemon;
        }

        // GET: api/pokemons/5
        [ResponseType(typeof(pokemon))]
        public async Task<IHttpActionResult> Getpokemon(int id)
        {
            pokemon pokemon = await db.pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return Ok(pokemon);
        }

        // PUT: api/pokemons/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpokemon(int id, pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pokemon.Id)
            {
                return BadRequest();
            }

            db.Entry(pokemon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!pokemonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/pokemons
        [ResponseType(typeof(pokemon))]
        public async Task<IHttpActionResult> Postpokemon(pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pokemon.Add(pokemon);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (pokemonExists(pokemon.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pokemon.Id }, pokemon);
        }

        // DELETE: api/pokemons/5
        [ResponseType(typeof(pokemon))]
        public async Task<IHttpActionResult> Deletepokemon(int id)
        {
            pokemon pokemon = await db.pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }

            db.pokemon.Remove(pokemon);
            await db.SaveChangesAsync();

            return Ok(pokemon);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool pokemonExists(int id)
        {
            return db.pokemon.Count(e => e.Id == id) > 0;
        }
    }
}
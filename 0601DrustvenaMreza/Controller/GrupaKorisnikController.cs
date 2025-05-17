using _0601DrustvenaMreza.Model;
using _0601DrustvenaMreza.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _0601DrustvenaMreza.Controller
{
    [Route("api/grupe/{grupaId}/korisnici")]
    [ApiController]
    public class GrupaKorisnikController : ControllerBase
    {
        private GrupaRepo grupaRepo = new GrupaRepo();
        private KorisnikRepo korisnikRepo = new KorisnikRepo();

        [HttpGet]
        public ActionResult<List<Korisnik>> Get(int grupaId)
        {
            if (!GrupaRepo.Data.ContainsKey(grupaId))
            {
                return NotFound();
            }

            List<Korisnik> sviKorisnici = KorisnikRepo.Data.Values.ToList();
            List<Korisnik> korisniciGrupe = new List<Korisnik>();
            foreach (Korisnik korisnik in sviKorisnici)
            {
                if (korisnik.grupe.Count != 0 && korisnik.grupe.ContainsKey(grupaId))
                {
                    korisniciGrupe.Add(korisnik);
                }
            }

            return Ok(korisniciGrupe);
        }

        // Ubaciti korisnika u grupu
        [HttpPut]
        public ActionResult<Korisnik> Put(int grupaId, [FromBody] int korisnikId)
        {
            if (!GrupaRepo.Data.ContainsKey(grupaId))
            {
                return NotFound("Grupa nije pronađena");
            }

            if (!KorisnikRepo.Data.ContainsKey(korisnikId))
            {
                return NotFound("Korisnik nije pronađen");
            }

            Korisnik korisnik = KorisnikRepo.Data[korisnikId];
            if (korisnik.grupe.ContainsKey(grupaId))
            {
                return BadRequest("Korisnik je već član grupe");
            }

            korisnik.grupe.Add(grupaId, GrupaRepo.Data[grupaId]);
            return Ok(korisnik);
        }

        // Izbaciti korisnika iz grupe
        [HttpDelete("{korisnikId}")]
        public ActionResult<Korisnik> Delete(int grupaId, int korisnikId)
        {
            if (!GrupaRepo.Data.ContainsKey(grupaId))
            {
                return NotFound("Grupa nije pronađena");
            }

            if (!KorisnikRepo.Data.ContainsKey(korisnikId))
            {
                return NotFound("Korisnik nije pronađen");
            }

            Korisnik korisnik = KorisnikRepo.Data[korisnikId];
            if (!korisnik.grupe.ContainsKey(grupaId))
            {
                return BadRequest("Korisnik nije član grupe");
            }

            korisnik.grupe.Remove(grupaId);
            return Ok(korisnik);
        }
    }
}

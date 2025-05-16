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
    }
}

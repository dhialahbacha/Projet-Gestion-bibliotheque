using Bll_Service.Service;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UILayer_Presentation.Controllers
{
    public class LivreController : Controller
    {

        private readonly ILivreService ls;

        public LivreController(ILivreService lss)
        {
            this.ls = lss;
        }



        
        public ActionResult ListeLivre()
        {
            List<Livre> listeLivres = ls.GetAllLivres();
            return View(listeLivres);
        }



       
        [HttpGet]
        public ActionResult AjoutLivre()
        {
            return View();
        }


        
        [HttpPost]
        public ActionResult AjoutLivre(Livre liv)
        {
           
            if (string.IsNullOrEmpty(liv.Auteur)) 
            {
              
                ModelState.AddModelError("Auteur", "Le champs de l'auteur est obligatoire");
            }

            if (string.IsNullOrEmpty(liv.Titre)) 
            {
               
                ModelState.AddModelError("Titre", "Le champs du titre est obligatoire");
            }

           
            if (string.IsNullOrEmpty(Convert.ToString(liv.Annee)) || liv.Annee < 1000 ) 
            { 

                ModelState.AddModelError("Année", "L'année est obligatoire");
            }

           
            if (ModelState.IsValid)
            {
                int verif = ls.AjoutLivre(liv);
                if (verif != 0)
                {
                   
                    return RedirectToAction("ListeLivre");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View();

                }
            }

            return View(); 


        }




       
        public ActionResult FindLivre()
        {
            return View();
        }


        
        [HttpPost]
        public ActionResult FindLivre(int id) 
        {
           
            Livre efind = ls.GetLivresById(id);
            if (efind != null)
            {
                return View(efind);
            }
            else
            {
                ViewBag.Msg = "Le livre n'existe pas";
                return View();
            }

        }







       

        public ActionResult ModifLivre()
        {
            return View();
        }


       
        [HttpPost]
        public ActionResult ModifLivre(Livre liv)
        {
            int verif = ls.ModifLivre(liv);
            if (verif != 0)
            {
              
                return RedirectToAction("ListeLivre");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }


        public ActionResult ModifLivreEmprunteur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModifLivreEmprunteur(Livre liv)
        {
            int verif = ls.ModifLivre(liv);
            if (verif != 0)
            {
              
                return RedirectToAction("ListeLivresEmprunteur");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }



        public ActionResult ModifLivLink(int id)
        {
          
            var eFind = ls.GetLivresById(id);

          
            return View("ModifLivre", eFind);
        }


        public ActionResult ModifLivLink2(int id)
        {
           
            var eFind = ls.GetLivresById(id);

           
            return View("ModifLivreEmprunteur", eFind);
        }






        [HttpGet]
        public ActionResult SuppLivLink(int id)
        {
            
            int verif = ls.SuppLivre(id);

           
            return RedirectToAction("ListeLivre");
        }


        [HttpGet]
        public ActionResult SuppLivLink2(int id)
        {
          
            int verif = ls.SuppLivre(id);

            
            return RedirectToAction("ListeLivresEmprunteur");
        }




       
        public ActionResult FindLivresEmprunteur()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ListeLivresEmprunteur(int id) 
        {
            
            List<Livre> listeLivre = ls.GetLivresByIdEmprunteur(id);
            if (listeLivre != null)
            {
                return View(listeLivre);
            }
            else
            {
                ViewBag.Msg = "L'Id de l'emprunteur n'existe pas";
                return View();
            }

        }

        public ActionResult ListeLivresEmprunteur() 
        {
            Emprunteur emp = (Emprunteur)Session["emprunteurSession2"];

            List<Livre> listeLivre = ls.GetLivresByIdEmprunteur(emp.Id);
            if (listeLivre != null)
            {
                return View(listeLivre);
            }
            else
            {
                ViewBag.Msg = "L'Id de l'emprunteur n'existe pas";
                return View();
            }

        }





        [HttpGet]
        public ActionResult AjoutLivreEmprunteur()
        {
            var emp = (Emprunteur)Session["emprunteurSession1"];
            Livre liv = new Livre();
            liv.Id = emp.Id;
            return View(liv);
        }


       
        [HttpPost]
        public ActionResult AjoutLivreEmprunteur(Livre liv)
        {
           
            if (string.IsNullOrEmpty(liv.Auteur)) 
            {
               
                ModelState.AddModelError("Auteur", "Le champs de l'auteur est obligatoire");
            }

            if (string.IsNullOrEmpty(liv.Titre)) 
            {
               
                ModelState.AddModelError("Titre", "Le champs du titre est obligatoire");
            }

            
            if (string.IsNullOrEmpty(Convert.ToString(liv.Annee)) || Convert.ToString(liv.Annee).Length < 4 || Convert.ToString(liv.Annee).Length > 4)  
            {
                ModelState.AddModelError("Année", "L'année est obligatoire");
            }

           
            if (ModelState.IsValid)
            {
                int verif = ls.AjoutLivre(liv);
                if (verif != 0)
                {
                    
                    
                    return RedirectToAction("ListeLivreEmp", "Emprunteur");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View();

                }
            }

            return View(); 


        }

    }
}
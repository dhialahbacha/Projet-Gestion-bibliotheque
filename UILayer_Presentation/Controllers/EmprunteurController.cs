using Bll_Service.Service;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UILayer_Presentation.Controllers
{
    public class EmprunteurController : Controller
    {
    
        private readonly IEmprunteurService es;

        public EmprunteurController(IEmprunteurService ess)
        {
            this.es = ess;
        }



        public ActionResult ListeEmrpunteur()
        {
            List<Emprunteur> listeEmprunteurs =  es.GetAllEmprunteurs();
            
            return View(listeEmprunteurs); 
        }




       
        [HttpGet]
        public ActionResult AjoutEmprunteur()
        {
            return View();
        }

     
        [HttpPost]
        public ActionResult AjoutEmprunteur(Emprunteur emp)
        {
            
            if (string.IsNullOrEmpty(emp.Nom)) 
            {
                
                ModelState.AddModelError("Nom", "Le champs du nom est obligatoire");
            }

            if (string.IsNullOrEmpty(emp.Prenom)) /
            {
                
                ModelState.AddModelError("Prenom", "Le champs du prenom est obligatoire");
            }


            if (string.IsNullOrEmpty(Convert.ToString(emp.AnneeNaissance)) | emp.AnneeNaissance < 1000 ) 
            { 

                ModelState.AddModelError("AnnéeNaissance", "L'année de naissance est obligatoire");
            }

            
            if (ModelState.IsValid)
            {
                int verif = es.AjoutEmprunteur(emp);
                if (verif != 0)
                {
                   
                    return RedirectToAction("ListeEmrpunteur");
                }
                else
                {
                    ViewBag.Msg = "L'ajout est KO!";
                    return View(); 

                }
            }
            
            return View(); 


        }




        public ActionResult FindEmprunteur()
        {
            return View();
        }


 
        [HttpPost]
        public ActionResult FindEmprunteur(int id)
        {
           
            Emprunteur efind = es.GetEmprunteurById(id);
            if (efind != null)
            {
                return View(efind);
            }
            else
            {
                ViewBag.Msg = "L'emprunteur n'existe pas";
                return View();
            }

        }



   

        public ActionResult ModifEmprunteur()
        {
            return View();
        }


       
        [HttpPost]
        public ActionResult ModifEmprunteur(Emprunteur emp)
        {
            int verif = es.ModifEmprunteur(emp);
            if (verif != 0)
            {
               
                return RedirectToAction("ListeEmrpunteur");
            }
            else
            {
                ViewBag.Msg = "La modification a échouée !!!";
                return View();
            }

        }



      
        public ActionResult ModifEmpLink(int id)
        {
          
            var eFind = es.GetEmprunteurById(id);

          
            return View("ModifEmprunteur", eFind);
        }




        

        [HttpGet]
        public ActionResult SuppEmpLink(int id)
        {
            
            int verif = es.SuppEmprunteur(id);

            
            return RedirectToAction("ListeEmrpunteur");
        }





        public ActionResult FindLivreEmp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListeLivreEmp(int id)
        {
            List<Livre> listeLivresEmp = es.GetAllLivreEmprunteurId(id);

            return View(listeLivresEmp);
        }

        public ActionResult ListeLivreEmp()
        {
            var emp = (Emprunteur)Session["emprunteurSession1"];
            List<Livre> listeLivresEmp = es.GetAllLivreEmprunteurId(emp.Id);

            return View(listeLivresEmp);
        }



        
        public ActionResult AjoutLivreLink(int id)
        {
           
            var eFind = es.GetEmprunteurById(id);
            Session["emprunteurSession1"] = eFind;

            
            return RedirectToAction("AjoutLivreEmprunteur", "Livre");
        }



        

        public ActionResult SuppLivreLink(int id)
        {
            var eFind = es.GetEmprunteurById(id);
            Session["emprunteurSession2"] = eFind;

            return RedirectToAction("ListeLivresEmprunteur", "Livre");
        }
    }
}
using CardiologicalResearch.Controllers;
using CardiologicalResearch.Models;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace CardiologicalResearch
{
    public class ControllerFactory : IControllerFactory
    {
        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            IRepository repository = new EntityRepository();
            Controller controller;
            if (controllerName == "Home")
            {
                controller = new HomeController(repository);
            }
            else if(controllerName == "Measurement")
            {
                controller = new MeasurementController(repository);
            }
            else if (controllerName == "MedicalRecord")
            {
                controller = new MedicalRecordController(repository);
            }
            else if (controllerName == "Examination")
            {
                controller = new ExaminationController(repository);
            }
            else if (controllerName == "ExaminationResult")
            {
                controller = new ExaminationResultController(repository);
            }
            else
            {
                controller = new HomeController(repository);
            }
            return controller;
        }
        public SessionStateBehavior GetControllerSessionBehavior(
            System.Web.Routing.RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }
    }
}
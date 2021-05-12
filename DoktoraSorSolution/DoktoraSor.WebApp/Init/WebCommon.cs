using DoktoraSor.Common;
using DoktoraSor.Entities;
using DoktoraSor.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoktoraSor.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {  
            DoctorUser duser = CurrentSession.UserDoctor;
            PatientUser puser = CurrentSession.UserPatient;
            if (duser!=null)
                {
                    return duser.Username;
                }         
               else if (puser!=null)
            {   
                return puser.Username;
            }

            return "system";

            }
    }
}
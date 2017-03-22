using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OuiHop.Helpers
{
    public class ValidationManager
    {

        // validates email and return true or false
        public static bool ValidateEmail(string email)
        {
            return Regex.IsMatch(email,
                   @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }

        // validates user info fields and return error message
        public static string ValidateBasicUserInfo(string email, string pass, string country ,string phone)
        {
            string errorMessage = "";

            if (pass != "" && country != "" && email != "" && phone != "")
            {

                if (ValidateEmail(email) == false)
                    errorMessage = "L’adresse email renseignée n’est pas valide, veuillez entrer une autre adresse.";
                else
                {
                    /* if (AppManager.MyAppManager.FacebookLogin == false)
                     {
                         if (pass == "")
                             errorMessage = "Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.";
                     }*/
                }
            }
            else
            {
                errorMessage = "Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.";
            }
            return errorMessage;
        }

        public static string ValidateUserProfileInfo(string fn, string ln, string gender, string birthYear)
        {
            string errorMessage = "";

            if (fn != "" && ln != "" && gender != "" && birthYear != "")
            {
                errorMessage = "";
            }
            else
            {//Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.  
                errorMessage = "Please fill in the fields in red to continue your registration.";
            }
            return errorMessage;
        }
        public static string ValidateAddresses(string Company, string HomeAdd, string WorkAdd)
        {
            string errorMessage = "";

            if (Company != "" && HomeAdd != "" && WorkAdd != "")
            {
                errorMessage = "";
            }
            else
            {//Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.  
                errorMessage = "Please fill in the fields in red to continue your registration.";
            }
            return errorMessage;
        }
        public static string ValidateCarInfo(string carManf,string carColor)
        {
            string errorMessage = "";
            if (carManf != "" && carColor != "")
            {
                errorMessage = "";
            }
            else
            {
                //Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.  
                errorMessage = "Please fill in the fields in red to continue your registration.";
            }
            return errorMessage;
        }
        public string ValidateUserInfo(string email, string pass) //string pass2)
        {
            string errorMessage = "";

            if (email != "") //&& pass2 != "")
            {

                if (ValidateEmail(email) == false)
                    errorMessage = "L’adresse email renseignée n’est pas valide, veuillez entrer une autre adresse.";
                else
                {
                    /*       if (AppManager.MyAppManager.FacebookLogin == false)
                           {
                               if (pass == "")
                                   errorMessage = "Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.";
                           }*/
                }

            }
            else
            {
                errorMessage = "Veuillez renseigner les champs en rouge afin de poursuivre votre inscription.";
            }

            return errorMessage;

        }

        /* public bool CompareUsersInfo(User user1, User user2)
         {
             if (user1 != null && user2 != null)
             {
                 if (user1.email == user2.email && user1.firstName == user2.firstName && user1.lastName == user2.lastName &&
                 user1.gender == user2.gender && user1.address == user2.address && user1.birthDate.Value.Date == user2.birthDate.Value.Date
                 && user1.mobileNo == user2.mobileNo && (user2.newPassword == null || user2.newPassword == "") && user1.country == user2.country)
                 {
                     bool groupAndLine = false;

                     if (user1.userGroup == null && user2.userGroup == null)
                         groupAndLine = true;
                     else if (user1.userGroup == null && user2.userGroup != null || user1.userGroup != null && user2.userGroup == null)
                         groupAndLine = false;
                     else if (user1.userGroup.groupName == user2.userGroup.groupName)
                         groupAndLine = true;
                     else
                         groupAndLine = false;

                     if (groupAndLine == true)
                     {
                         if (user1.userLine == null && user2.userLine == null)
                             return true;
                         else if (user1.userLine == null && user2.userLine != null)
                         {
                             if (user2.userLine.name == null)
                                 return true; // autres (the same)
                             else
                                 return false;
                         }
                         else if (user1.userLine != null && user2.userLine == null)
                         {
                             if (user1.userLine.name == null) // autres
                                 return true;
                             else
                                 return false;
                         }

                         else if (user1.userLine != null && user2.userLine.name == null) // line to autres
                         {
                             return false;
                         }
                         else // user line != null in both
                         {
                             if (user1.userLine.name == user2.userLine.name || user1.userLine.name == null && user2.userLine.name == null)
                                 return true;
                             else
                                 return false;
                         }


                     }
                     else
                         return false;

                 }
                 else
                     return false;
             }
             else
                 return false;

         }

         public bool CompareCars(Car car1, Car car2)
         {
             if (car1 != null && car2 != null)
             {
                 if (car1.make == car2.make && car1.model == car2.model && car1.plateNumber == car2.plateNumber && car1.color == car2.color)
                     return true;
                 else
                     return false;
             }
             else if (car1 == null && car2 == null)
                 return true; // nothing changed
             else
                 return false;

         }

         public bool CompareImages(BitmapImage image1, BitmapImage image2)
         {
             if (image1 != null && image2 != null)
             {
                 byte[] array1 = ConvertToBytes(image1);
                 byte[] array2 = ConvertToBytes(image2);

                 if (array1.SequenceEqual(array2))
                     return true;
                 else
                     return false;
             }
             else if (image1 == null && image2 == null)
                 return true;
             else if (image1 == null && image2 != null)
                 return false;
             else
                 return true;

         }

         // if equal so it is true, not equal is false
         public bool CompareTrips(List<Trip> tripsList1, List<Trip> tripsList2)
         {
             if ((tripsList1 != null && tripsList2 == null) || (tripsList1 == null && tripsList2 != null))
                 return false;
             else if (tripsList1 == null && tripsList2 == null)
                 return true;
             else //if (tripsList1 != null && tripsList2 != null)
             {
                 if (tripsList1.Count != tripsList2.Count)
                     return false;
                 else
                 {
                     tripsList1 = tripsList1.OrderBy(t => t.id).ToList();
                     tripsList2 = tripsList2.OrderBy(t => t.id).ToList();
                     bool result = true;
                     for (int i = 0; i < tripsList1.Count; i++)
                     {
                         if (tripsList1[i].id != tripsList2[i].id)
                         {
                             result = false;
                             break;
                         }

                     }

                     return result;
                 }

             }


         }


         public byte[] ConvertToBytes(BitmapImage bitmapImage)
         {
             byte[] data = null;
             using (MemoryStream stream = new MemoryStream())
             {
                 WriteableBitmap wBitmap = new WriteableBitmap(bitmapImage);
                 wBitmap.SaveJpeg(stream, wBitmap.PixelWidth, wBitmap.PixelHeight, 0, 100);
                 stream.Seek(0, SeekOrigin.Begin);
                 data = stream.GetBuffer();
             }

             return data;
         }*/
    }

}
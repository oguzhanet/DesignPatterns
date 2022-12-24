using System.Text;

namespace Template.DesignPattern.WebApp.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string GetPicture()
        {
            return $"<img src='{AppUser.PictureUrl}' class='card-img-top'>";
        }

        protected override string SetFooter()
        {
            StringBuilder footer = new StringBuilder();

            footer.Append("<a href='#' class='btn btn-primary'>Mesaj Gönder</a>");
            footer.Append("<a href='#' class='btn btn-primary'>Detaylı Profil</a>");

            return footer.ToString();
        }
    }
}

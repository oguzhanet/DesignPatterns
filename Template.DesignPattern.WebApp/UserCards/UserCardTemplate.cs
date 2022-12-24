using BaseProject.Identity.Entities;
using System.Text;

namespace Template.DesignPattern.WebApp.UserCards
{
    public abstract class UserCardTemplate
    {
        protected AppUser AppUser { get; set; }

        public void SetUser(AppUser appUser)
        {
            AppUser = appUser;
        }

        public string Build()
        {
            if (AppUser == null) throw new ArgumentNullException(nameof(AppUser));

            StringBuilder card = new StringBuilder();

            card.Append("<div class='card' style='width: 18rem; '>");
            card.Append(GetPicture());
            card.Append($@"<div class='card - body'>
                          <h5 class='card - title'>{AppUser.UserName}</h5>  
                          <p class='card - text'>{AppUser.Description}</p>");
            card.Append(SetFooter());
            card.Append("</div>");
            card.Append("</div>");

            return card.ToString();
        }

        protected abstract string SetFooter();

        protected abstract string GetPicture();
    }
}

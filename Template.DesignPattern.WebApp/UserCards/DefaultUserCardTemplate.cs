namespace Template.DesignPattern.WebApp.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {
        protected override string GetPicture()
        {
            return "<img src='/userPictures/defaultuser.png' class='card-img-top'>";
        }

        protected override string SetFooter()
        {
            return String.Empty;
        }
    }
}

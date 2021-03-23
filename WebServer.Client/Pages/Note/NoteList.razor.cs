using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebServer.Client.Pages.Note
{
    public partial class NoteList
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        public void MoveCreate()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/create");

        }

        public void MoveUpdate()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/update/"+1);

        }

        public void MoveDelete()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/delete/" + 1);

        }

        public void MoveDetail()
        {
            //this.NavigationManager.NavigateTo("/note/create", forceLoad: true);
            this.NavigationManager.NavigateTo("/note/detail/" + 1);

        }
    }
}

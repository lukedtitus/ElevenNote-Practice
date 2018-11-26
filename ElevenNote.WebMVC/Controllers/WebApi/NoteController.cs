using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebMVC.Controllers.WebApi
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState(int noteId, bool newState)
        {
            // create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);

            //get the note
            var detail = service.GetNoteById(noteId);

            //create the noteedit model instance with the new star state
            var updatedNote =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState
                };

            //return a value indicating whether the update succeeded
            return service.UpdateNote(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}

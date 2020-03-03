using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using tutoWF.Models;

namespace tutoWF
{
    public class EventsController : ApiController
    {
        [HttpGet]
        public IEnumerable<Event> Get(int id)
        {
            return DAL.DAL.GetEventsbyTeacherId(id);
        }

        [HttpGet]
        public Event Get(int event_id, int teacher_id)
        {
            return DAL.DAL.GetEventbyId(event_id, teacher_id);
        }

        [HttpPost]
        public int Post([FromBody]Event newEvent)
        {
            return DAL.DAL.CreateEvent(newEvent);
        }

        [HttpPut]
        public void Put(int id, [FromBody]Event updateEvent)
        {
            DAL.DAL.UpdateEvent(updateEvent);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            DAL.DAL.DeleteEvent(id);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using GymBoyAttendance.Models;

namespace GymBoyAttendance.Controllers
{
    public class AttendanceController : ApiController
    {
        private GymBoyAttendanceContext db = new GymBoyAttendanceContext();

        // GET api/Attendance
        public IEnumerable<Attendance> GetAttendances(string q = null, string sort = null, bool desc = false, int? limit = null, int offset = 0)
        {
            var list = ((IObjectContextAdapter)db).ObjectContext.CreateObjectSet<Attendance>();

            IQueryable<Attendance> items = string.IsNullOrEmpty(sort)
                ? list.OrderBy(o => o.TrainingLocation)
                : list.OrderBy(String.Format("it.{0} {1}", sort, desc ? "DESC" : "ASC"));

            if (!string.IsNullOrEmpty(q) && q != "undefined")
                items = items.Where(t => t.TrainingLocation.Contains(q));

            if (offset > 0) items = items.Skip(offset);
            if (limit.HasValue) items = items.Take(limit.Value);
            return items;
        }

        // GET api/Attendance/5
        public Attendance GetAttendance(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return attendance;
        }

        // PUT api/Attendance/5
        public HttpResponseMessage PutAttendance(int id, Attendance attendance)
        {
            if (ModelState.IsValid && id == attendance.ID)
            {
                db.Entry(attendance).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Attendance
        public HttpResponseMessage PostAttendance(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, attendance);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = attendance.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Attendance/5
        public HttpResponseMessage DeleteAttendance(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Attendances.Remove(attendance);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, attendance);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
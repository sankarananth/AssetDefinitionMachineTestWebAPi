using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AssetDefinitionMachineTest.Models;

namespace AssetDefinitionMachineTest.Controllers
{
    public class AssetDefsController : ApiController
    {
        private assetDBEntities db = new assetDBEntities();

        // GET: api/AssetDefs
        public List<AssetViewModel> GetAssetDefs()
        {
			List<AssetDef> assetList=db.AssetDefs.ToList();
			List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
			{
				assetId=Convert.ToInt32(x.ad_id),
				assetName = x.ad_name,
				assetType = x.Assettype.at_name,
				assetClass = x.ad_class
			}).ToList();
			return avList;
            
        }

        // GET: api/AssetDefs/5
        [ResponseType(typeof(AssetDef))]
        public IHttpActionResult GetAssetDef(int id)
        {
            AssetDef assetDef = db.AssetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            return Ok(assetDef);
        }
		public int Get(string name)
		{
			List<AssetDef> aslist = db.AssetDefs.Where(x => x.ad_name.Contains(name)).ToList();
			int count = aslist.Count;
			return count;
		}

        // PUT: api/AssetDefs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssetDef(decimal id, AssetDef assetDef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != assetDef.ad_id)
            {
                return BadRequest();
            }

            db.Entry(assetDef).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetDefExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AssetDefs
        [ResponseType(typeof(AssetDef))]
        public IHttpActionResult PostAssetDef(AssetDef assetDef)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AssetDefs.Add(assetDef);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assetDef.ad_id }, assetDef);
        }

        // DELETE: api/AssetDefs/5
        [ResponseType(typeof(AssetDef))]
        public IHttpActionResult DeleteAssetDef(int id)
        {
            AssetDef assetDef = db.AssetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            db.AssetDefs.Remove(assetDef);
            db.SaveChanges();

            return Ok(assetDef);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetDefExists(decimal id)
        {
            return db.AssetDefs.Count(e => e.ad_id == id) > 0;
        }
    }
}
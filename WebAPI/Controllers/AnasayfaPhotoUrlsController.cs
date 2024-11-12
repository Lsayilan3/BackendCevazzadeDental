
using Business.Handlers.AnasayfaPhotoUrls.Commands;
using Business.Handlers.AnasayfaPhotoUrls.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// AnasayfaPhotoUrls If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AnasayfaPhotoUrlsController : BaseApiController
    {
        ///<summary>
        ///List AnasayfaPhotoUrls
        ///</summary>
        ///<remarks>AnasayfaPhotoUrls</remarks>
        ///<return>List AnasayfaPhotoUrls</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AnasayfaPhotoUrl>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetAnasayfaPhotoUrlsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>AnasayfaPhotoUrls</remarks>
        ///<return>AnasayfaPhotoUrls List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AnasayfaPhotoUrl))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int anasayfaPhotoUrlId)
        {
            var result = await Mediator.Send(new GetAnasayfaPhotoUrlQuery { AnasayfaPhotoUrlId = anasayfaPhotoUrlId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add AnasayfaPhotoUrl.
        /// </summary>
        /// <param name="createAnasayfaPhotoUrl"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAnasayfaPhotoUrlCommand createAnasayfaPhotoUrl)
        {
            var result = await Mediator.Send(createAnasayfaPhotoUrl);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update AnasayfaPhotoUrl.
        /// </summary>
        /// <param name="updateAnasayfaPhotoUrl"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAnasayfaPhotoUrlCommand updateAnasayfaPhotoUrl)
        {
            var result = await Mediator.Send(updateAnasayfaPhotoUrl);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete AnasayfaPhotoUrl.
        /// </summary>
        /// <param name="deleteAnasayfaPhotoUrl"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAnasayfaPhotoUrlCommand deleteAnasayfaPhotoUrl)
        {
            var result = await Mediator.Send(deleteAnasayfaPhotoUrl);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addPhoto")]
        public async Task<IActionResult> AddPhoto([FromForm] AddPhotoCommad addPhoto)
        {

            var result = await Mediator.Send(addPhoto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}

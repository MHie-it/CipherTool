using CipherTool.Models;
using CipherTool.Service;
using Microsoft.AspNetCore.Mvc;

namespace CipherTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class cipherController : ControllerBase
    {
        private readonly cipherService _crypto;

        public cipherController(cipherService crypto)
        {
            _crypto = crypto;
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] cipherRequest req)
        {
            try
            {
                var result = _crypto.AesEncrypt(req.Text);
                return Ok(new cipherResponse { Success = true, Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new cipherResponse { Success = false, Error = ex.Message });
            }
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] cipherRequest req)
        {
            try
            {
                var result = _crypto.AesDecrypt(req.Text);
                return Ok(new cipherResponse { Success = true, Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new cipherResponse { Success = false, Error = ex.Message });
            }
        }
    }
}
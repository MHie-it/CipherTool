using CipherTool.Models;
using CipherTool.Service;
using Microsoft.AspNetCore.Mvc;

namespace CipherTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CipherController : ControllerBase
    {
        private readonly CipherService _crypto;

        public CipherController(CipherService crypto)
        {
            _crypto = crypto;
        }

        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] CipherRequest req)
        {
            try
            {
                var result = _crypto.AesEncrypt(req.Text);
                return Ok(new CipherResponse { Success = true, Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new CipherResponse { Success = false, Error = ex.Message });
            }
        }

        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] CipherRequest req)
        {
            try
            {
                var result = _crypto.AesDecrypt(req.Text);
                return Ok(new CipherResponse { Success = true, Result = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new CipherResponse { Success = false, Error = ex.Message });
            }
        }
    }
}
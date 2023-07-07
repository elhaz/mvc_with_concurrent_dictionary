using mvc_with_concurrent_dictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace mvc_with_concurrent_dictionary.Controllers
{
    public class DictController : ApiController {
        // GET : api/dict/session/
        [HttpGet]
        // session list 보기
        public IEnumerable<string> session() {
            return ConcurrentDirectoryAct.GetKeyList();
        }

        // GET : api/dict/key/
        // QS : session
        // description : session 에 대한 dict 반환
        [HttpGet]
        public Dictionary<string, string> key([FromUri] string session) {
            return ConcurrentDirectoryAct.Read(session);
        }

        // POST : api/dict/value/
        // QS : session
        // QS : key
        // QS : value
        // description : ConcurrentDirectory 에 value 생성
        [HttpPost]
        public void value([FromUri] string session, [FromUri] string key, [FromUri] string value) {
            ConcurrentDirectoryAct.Create(session, key, value);
        }

        // DELETE : api/dict/delete/
        // QS : session
        // QS : key
        // session 에서 key 또는 session 삭제
        [HttpDelete]
        public void delete([FromUri] string session, [FromUri] string key) {
            ConcurrentDirectoryAct.Delete(session, key);
        }
    }
}

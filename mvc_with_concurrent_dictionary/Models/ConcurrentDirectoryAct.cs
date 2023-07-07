using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace mvc_with_concurrent_dictionary.Models {
    public class ConcurrentDirectoryAct {
        // Global.concurrentDictionary 에 대하여 읽기, 쓰기, 삭제, 업데이트를 수행하는 메소드를 정의한다.


        // Global.concurrentDictionary 에 대하여 key list 를 반환하는 메소드를 정의한다.
        public static List<string> GetKeyList() {
            return Global.concurrentDictionary.Keys.ToList();
        }

        // Global.concurrentDictionary 에 대하여 key 에 대한 value 를 반환하는 메소드를 정의한다.
        public static Dictionary<string, string> Read(string session) {
            // key 가 있을경우 해당 key 의 value 를 반환
            Dictionary<string, string> value;
            Global.concurrentDictionary.TryGetValue(session, out value);
            return value;
        }

        // Global.concurrentDictionary 에 대하여 key 에 대한 value 를 추가하는 메소드를 정의한다.
        public static void Create(string session_key, string key, string value) {
            // Global.concurrentDictionary 의 key 에 session_key 가 없을경우
            if (!Global.concurrentDictionary.ContainsKey(session_key)) {
                // Global.concurrentDictionary 에 key 를 추가한다.
                Global.concurrentDictionary.TryAdd(session_key, new Dictionary<string, string>());
            }

            // Global.concurrentDictionary 의 key 에 key 가 없을경우
            if (!Global.concurrentDictionary[session_key].ContainsKey(key)) {
                // Global.concurrentDictionary 에 key 에 value 를 추가한다.
                Global.concurrentDictionary[session_key].Add(key, value);
            }

            // Global.concurrentDictionary 의 key 에 key 가 있을경우
            else {
                // Global.concurrentDictionary 에 key 에 value 를 업데이트한다.
                Global.concurrentDictionary[session_key][key] = value;
            }
        }

        // Global.concurrentDictionary 에 대하여 key 에 대한 value 를 삭제하는 메소드를 정의한다.
        public static void Delete(string session_key, string key) {
            // Global.concurrentDictionary 의 key 에 session_key 가 있을경우
            if (Global.concurrentDictionary.ContainsKey(session_key)) {
                // Global.concurrentDictionary 의 key 에 key 가 있을경우
                if (Global.concurrentDictionary[session_key].ContainsKey(key)) {
                    // Global.concurrentDictionary 의 key 에서 key 를 삭제한다.
                    Global.concurrentDictionary[session_key].Remove(key);
                }
            }
        }

        // Global.concurrentDictionary 에 대하여 key 를 삭제하는 메소드를 정의한다.
        public void Delete(string session_key) {
            // Global.concurrentDictionary 의 key 에 session_key 가 있을경우
            if (Global.concurrentDictionary.ContainsKey(session_key)) {
                // Global.concurrentDictionary 에서 key 를 삭제한다.
                Global.concurrentDictionary.TryRemove(session_key, out Dictionary<string, string> value);
            }
        }
    }
}
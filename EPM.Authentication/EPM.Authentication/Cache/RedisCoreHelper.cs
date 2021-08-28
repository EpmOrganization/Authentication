using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Authentication.Cache
{
    public class RedisCoreHelper
    {
        //static ILog _log = LogManager.GetLogger(typeof(RedisStackClient));

        public static string RedisConnStr { get; set; }
        public static void SetRedisConnStr(string redisConnStr)
        {
            RedisConnStr = redisConnStr;
        }

        private static readonly object Locker = new object();

        private ConnectionMultiplexer redisMultiplexer;

        IDatabase redisDB = null;

        private static RedisCoreHelper _instance = null;
        public static RedisCoreHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new RedisCoreHelper();
                        }
                    }
                }
                return _instance;
            }
        }

        public RedisCoreHelper()
        {
            try
            {
                redisMultiplexer = ConnectionMultiplexer.Connect(RedisConnStr);
                redisDB = redisMultiplexer.GetDatabase();
            }
            catch (Exception ex)
            {
                //_log.Error("StatckRedis实例化失败" + ex.ToString());
                redisMultiplexer = null;
                redisDB = null;
            }
        }

        public bool TryGetValue<T>(string key, out T entity) where T : class, new()
        {
            entity = null;
            try
            {
                entity = null;
                if (redisDB == null)
                {
                    //StackRedis初始化
                    new RedisCoreHelper();
                }
                string v = redisDB.StringGet(key);
                if (String.IsNullOrEmpty(v))
                    return false;
                entity = JsonConvert.DeserializeObject<T>(v);
                return true;
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
                return false;
            }
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    // InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                value = redisDB.StringGet(key);
                return value != null;
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
                return false;
            }
        }

        public bool TryGetValue(string key, out int value)
        {

            value = 0;
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    //InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                var keyValue = redisDB.StringGet(key);
                if (string.IsNullOrEmpty(keyValue))
                    return false;
                value = int.Parse(keyValue);
                return true;
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
                return false;
            }
        }


        public T Get<T>(string key) where T : class, new()
        {
            T entity = null;
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    // InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                string keyValue = redisDB.StringGet(key);
                if (string.IsNullOrEmpty(keyValue))
                    return null;
                entity = keyValue as T;
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
            return entity;
        }

        public string Get(string key)
        {
            string value = null;
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    //InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                value = redisDB.StringGet(key);
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
            return value;
        }

        /// <summary>
        /// 键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ExistKey(string key)
        {
            if (redisDB == null)
            {
                //StackRedis初始化
                //InitConnect(RedisCoreHelper.RedisConnStr);
                new RedisCoreHelper();
            }
            return redisDB.KeyExists(key);
        }

        public string GetSet(string key, string newValue)
        {
            string value = null;
            try
            {
                if (ExistKey(key))
                    value = redisDB.StringGet(key);
                else
                {
                    redisDB.StringSet(key, newValue);
                    value = newValue;
                }
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
            return value;
        }

        public T GetSet<T>(string key, T newValue) where T : class, new()
        {
            T value = null;
            try
            {
                if (ExistKey(key))
                {
                    value = Get<T>(key);
                }
                else
                {
                    redisDB.StringSet(key, JsonConvert.SerializeObject(newValue));
                    value = newValue;
                }
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
            return value;
        }

        public async Task SetValueAsync<T>(string key, T entity)
        {
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    // InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                if (entity == null || entity.GetType().IsAssignableFrom(typeof(string)))
                    await redisDB.StringSetAsync(key, entity as string);
                else
                    await redisDB.StringSetAsync(key, JsonConvert.SerializeObject(entity));
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="seconds">过期时间(秒)</param>
        public void SetValue<T>(string key, T entity, int seconds)
        {
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    //InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                if (entity == null || entity.GetType().IsAssignableFrom(typeof(string)))
                    redisDB.StringSet(key, entity as string, TimeSpan.FromSeconds(seconds));
                else
                    redisDB.StringSet(key, JsonConvert.SerializeObject(entity), TimeSpan.FromSeconds(seconds));
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="seconds">过期时间(秒)</param>
        public async Task SetValueAsync<T>(string key, T entity, int seconds)
        {
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    //InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                if (entity == null || entity.GetType().IsAssignableFrom(typeof(string)))
                    await redisDB.StringSetAsync(key, entity as string, TimeSpan.FromSeconds(seconds));
                else
                    await redisDB.StringSetAsync(key, JsonConvert.SerializeObject(entity), TimeSpan.FromSeconds(seconds));
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
        }

        public async Task IncrByAsync(string key, long num)
        {
            try
            {
                if (redisDB == null)
                {
                    //StackRedis初始化
                    //InitConnect(RedisCoreHelper.RedisConnStr);
                    new RedisCoreHelper();
                }
                await redisDB.StringIncrementAsync(key, num);
            }
            catch (Exception ex)
            {
                //_log.Error(ex);
            }
        }

        public async void RemoveAsync(params string[] keys)
        {
            if (redisDB == null)
            {
                //StackRedis初始化
                //InitConnect(RedisCoreHelper.RedisConnStr);
                new RedisCoreHelper();
            }
            await Task.Run(() =>
            {
                if (keys != null && keys.Length > 0)
                {
                    foreach (var item in keys)
                    {
                        redisDB.KeyDelete(item);
                    }
                }
            });
        }
    }
}

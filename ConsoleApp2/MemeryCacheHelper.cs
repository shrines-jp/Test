using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;

namespace ConsoleApp2
{
    /// <summary>
    /// CachePriority 
    /// </summary>
    public enum CachePriority
    {
        Default,
        NotRemovable
    }

    /// <summary>
    /// 2013.10.15 박정환
    /// 메모리캐쉬 헬퍼 클래스
    /// CodeProject 참조
    /// System.Runtime.Caching.MemoryCache is threadsafe. 
    /// Multiple concurrent threads can read and write a MemoryCache instance. 
    /// Internally thread-safety is automatically handled to ensure the cache is updated in a consistent manner.
    /// What this might be referring to is that data stored within the cache may itself not be threadsafe. 
    /// For example if a List is placed in the cache, and two separate threads both get a reference to the cached List, 
    /// the two threads will end up stepping on each other if they both attempt to update the list simultaneously.
    /// </summary>
    public class MemoryCacheHelper
    {
        // Gets a reference to the default MemoryCache instance. 
        private static readonly ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;


        /// <summary>
        /// AddToCache
        /// T타입을 사용하기 위해서, 메소드 오버라이딩 추가함
        /// 2013.10.15 박정환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CacheKeyName"></param>
        /// <param name="value"></param>
        /// <param name="MyCacheItemPriority"></param>
        public void AddToCache<T>(string CacheKeyName, T value, CachePriority MyCacheItemPriority)
        {
            //Type type = typeof(T);
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60.00),
                RemovedCallback = callback
            };
            // Add inside cache 
            cache.Set(CacheKeyName, value, policy);
        }


        /// <summary>
        /// AddToCache
        /// T타입을 사용하기 위해서, 메소드 오버라이딩 추가함
        /// 2013.10.15 박정환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="CacheKeyName"></param>
        /// <param name="value"></param>
        /// <param name="dtOffsetMinutes"></param>
        /// <param name="MyCacheItemPriority"></param>
        public void AddToCache<T>(string CacheKeyName, T value, double dtOffsetMinutes, CachePriority MyCacheItemPriority)
        {
            //Type type = typeof(T);
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(dtOffsetMinutes),
                RemovedCallback = callback
            };
            // Add inside cache 
            cache.Set(CacheKeyName, value, policy);
        }


        /// <summary>
        /// 기본캐쉬등록
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <param name="CacheItem"></param>
        /// <param name="MyCacheItemPriority"></param>
        public void AddToCache(string CacheKeyName, object CacheItem, CachePriority MyCacheItemPriority)
        {
            // 
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60.00),
                RemovedCallback = callback
            };

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }


        /// <summary>
        /// 시간설정가능 캐쉬등록
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <param name="CacheItem"></param>
        /// <param name="dtOffsetMinutes"></param>
        /// <param name="MyCacheItemPriority"></param>
        public void AddToCache(string CacheKeyName, object CacheItem, double dtOffsetMinutes, CachePriority MyCacheItemPriority)
        {
            // 
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(dtOffsetMinutes),
                RemovedCallback = callback
            };

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }


        /// <summary>
        /// 캐쉬를 파일에 쓰는 설정
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <param name="CacheItem"></param>
        /// <param name="MyCacheItemPriority"></param>
        /// <param name="FilePath"></param>
        public void AddToCache(string CacheKeyName, object CacheItem, CachePriority MyCacheItemPriority, List<string> FilePath)
        {
            // 
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60.00),
                RemovedCallback = callback
            };
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(FilePath));

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }


        /// <summary>
        /// 시간 설정가능 캐쉬를 파일에 쓰는 설정
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <param name="CacheItem"></param>
        /// <param name="MyCacheItemPriority"></param>
        /// <param name="FilePath"></param>
        public void AddToCache(string CacheKeyName, object CacheItem, double dtOffsetMinutes, CachePriority MyCacheItemPriority, List<string> FilePath)
        {
            // 
            callback = new CacheEntryRemovedCallback(this.CachedItemRemovedCallback);
            policy = new CacheItemPolicy
            {
                Priority = (MyCacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(dtOffsetMinutes),
                RemovedCallback = callback
            };
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(FilePath));

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }


        /// <summary> 
        /// Get an object from cache 
        /// T타입을 사용하기 위해서, 메소드 오버라이딩 추가함
        /// 2013.10.15 박정환
        /// </summary> 
        /// <typeparam name="T">Type of object</typeparam> 
        /// <param name="key">Name of key in cache</param> 
        /// <returns>Object from cache</returns> 
        public T GetCached<T>(string CacheKeyName) where T : class
        {
            _ = typeof(T);
            return (T)cache[CacheKeyName];
        }


        /// <summary>
        /// 캐쉬데이터 가져오기
        /// </summary>
        /// <param name="CacheKeyName">캐쉬 키</param>
        /// <returns></returns>
        public object GetCached(string CacheKeyName)
        {
            return cache[CacheKeyName] as object;
        }

        /// <summary>
        /// 존재여부 확인
        /// </summary>
        /// <param name="CacheKeyName"></param>
        /// <returns></returns>
        public bool Contains(string CacheKeyName)
        {
            return cache.Contains(CacheKeyName);
        }


        /// <summary>
        /// 캐쉬제거
        /// </summary>
        /// <param name="CacheKeyName"></param>
        public void RemoveCached(string CacheKeyName)
        {
            if (cache.Contains(CacheKeyName))
            {
                cache.Remove(CacheKeyName);
            }
        }

        /// <summary>
        /// 캐쉬항목제거 콜백이벤트
        /// </summary>
        /// <param name="arguments"></param>
        private void CachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            string strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), "| Key-Name: ", arguments.CacheItem.Key, " | Value-Object: ", arguments.CacheItem.Value.ToString());
            Trace.WriteLine(strLog);
        }
    }
}
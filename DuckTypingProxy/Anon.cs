using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckTypingProxy
{
    public static class Anon
    {
        public static Delegate Method<TResult>(Func<TResult> method) { return method; }
        public static Delegate Method<T1, TResult>(Func<T1, TResult> method) { return method; }
        public static Delegate Method<T1, T2, TResult>(Func<T1, T2, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> method) { return method; }

        public static Delegate Method(Action method) { return method; }
        public static Delegate Method<T1>(Action<T1> method) { return method; }
        public static Delegate Method<T1, T2>(Action<T1, T2> method) { return method; }
        public static Delegate Method<T1, T2, T3>(Action<T1, T2, T3> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4>(Action<T1, T2, T3, T4> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> method) { return method; }
        public static Delegate Method<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> method) { return method; }    
    
        public static Delegate[] Methods(params Delegate[] methods)
        {
            return methods;
        }
    }
}

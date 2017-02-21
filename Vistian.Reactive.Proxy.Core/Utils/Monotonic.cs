/*
 * Copyright (c) 2008 Markus Olsson 
 * var mail = string.Join(".", new string[] {"j", "markus", "olsson"}) + string.Concat('@', "gmail.com");
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this 
 * software and associated documentation files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Vistian.Reactive.Proxy.Utils
{
    /// <summary>
    /// Provides a consistently increasing values for time.
    /// </summary>
    /// <remarks>All methods of this class is guaranteed to be thread safe</remarks>
    internal static class Monotonic
    {
        private static readonly Stopwatch StopWatchTimer;

        /// <summary>
        /// Gets the number of elapsed milliseconds since first initialized.
        /// </summary>
        /// <remarks>This method is thread safe</remarks>
        public static long Now => Time();

        static Monotonic()
        {
            StopWatchTimer = Stopwatch.StartNew();
        }

        /// <summary>
        /// Gets the number of elapsed milliseconds since first initialized.
        /// Guaranteed to be a non-negative increasing number.
        /// </summary>
        public static long Time()
        {
            return StopWatchTimer.ElapsedMilliseconds;
        }
    }
}

#region header

// -----------------------------------------------------------------------
//  <copyright file="UnitOfWorkScopedObjectContent.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

using NStack.Data;

namespace NStack.WebApi
{
    public class UnitOfWorkScopedObjectContent : ObjectContent
    {
        private readonly IUnitOfWorkScope _scope;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:System.Net.Http.ObjectContent" /> class.
        /// </summary>
        /// <param name="type">The type of object this instance will contain.</param>
        /// <param name="value">The value of the object this instance will contain.</param>
        /// <param name="formatter">The formatter to use when serializing the value.</param>
        /// <param name="scope">The current unit of work scope.</param>
        public UnitOfWorkScopedObjectContent(Type type, object value, MediaTypeFormatter formatter, IUnitOfWorkScope scope)
            : base(type, value, formatter)
        {
            _scope = scope;
        }

        protected async override System.Threading.Tasks.Task SerializeToStreamAsync(System.IO.Stream stream, System.Net.TransportContext context)
        {
            await base.SerializeToStreamAsync(stream, context);
            _scope.Commit();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) _scope.Dispose();
        }
    }
}
#region header
// <copyright file="RootEntity.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>
#endregion
namespace MG.Models
{
    /// <summary>
    /// A generic root entity for entities that use an integer as an ID.
    /// </summary>
    public abstract class RootEntity : RootEntity<int>
    {
         
    }
}
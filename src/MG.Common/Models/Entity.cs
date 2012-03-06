#region header
// <copyright file="Entity.cs" company="mikegrabski.com">
//      Copyright (c) 2012 Mike Grabski. All rights reserved.
// </copyright>
#endregion
namespace MG.Models
{
    /// <summary>
    /// A generic root entity for entities that use an integer as an ID.
    /// </summary>
    public abstract class Entity : Entity<int>
    {
         
    }
}
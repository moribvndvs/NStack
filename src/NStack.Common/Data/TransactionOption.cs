#region header
// -----------------------------------------------------------------------
//  <copyright file="TransactionOption.cs" company="Family Bronze, LTD">
//      © 2013 Mike Grabski and Family Bronze, LTD All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------
#endregion
namespace NStack.Data
{
    /// <summary>
    /// Indicates how to handle transactions when using a unit of work scope.
    /// </summary>
    public enum TransactionOption
    {
        /// <summary>
        /// Will join an ambient transaction, otherwise one will be created.
        /// </summary>
        Required,
        /// <summary>
        /// Always creates a new transaction.
        /// </summary>
        RequiresNew,
        /// <summary>
        /// Never uses a transaction.
        /// </summary>
        Suppress
    }
}
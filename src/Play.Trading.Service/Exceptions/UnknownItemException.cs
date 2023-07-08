using System;
using System.Runtime.Serialization;

namespace Play.Trading.Service.Exceptions
{
    [Serializable]
    internal class UnknownItemException : Exception
    {
        public UnknownItemException(Guid itemId) : base($"Unknown item '{itemId}'")
        {
            this.itemId = itemId;
        }

        public Guid itemId { get; }
    }
}
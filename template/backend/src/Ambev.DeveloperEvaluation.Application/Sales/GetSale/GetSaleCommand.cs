﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The unique identifier of the Sale to retrieve
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of GetSaleCommand
        /// </summary>
        /// <param name="id">The ID of the Sale to retrieve</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
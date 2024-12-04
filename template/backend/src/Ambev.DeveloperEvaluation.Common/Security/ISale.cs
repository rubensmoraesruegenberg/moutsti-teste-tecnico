using System;

namespace Ambev.DeveloperEvaluation.Common.Security
{
    /// <summary>
    /// Define o contrato para representação de uma venda no sistema.
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Obtém o identificador único da venda.
        /// </summary>
        /// <returns>O ID da venda como uma string.</returns>
        public string Id { get; }

        /// <summary>
        /// Obtém o identificador do usuário que realizou a venda.
        /// </summary>
        /// <returns>O ID do usuário como uma string.</returns>
        public Guid IdUser { get; }

        /// <summary>
        /// Obtém a data da venda.
        /// </summary>
        /// <returns>A data da venda como uma string.</returns>
        public DateTime Date { get; }

        /// <summary>
        /// Obtém o valor total da venda.
        /// </summary>
        /// <returns>O valor total da venda como uma string.</returns>
        public decimal TotalAmount { get; }
    }
}

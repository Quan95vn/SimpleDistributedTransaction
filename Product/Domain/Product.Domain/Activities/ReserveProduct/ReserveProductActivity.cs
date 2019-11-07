using MassTransit.Courier;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Activities.ReserveProduct
{
    public class ReserveProductActivity : Activity<ReserveProductArguments, ReserveProductLog>
    {
        public ReserveProductActivity()
        {

        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ReserveProductArguments> context)
        {
            var a = context.Arguments.OrderDetails;

            return context.Completed();
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ReserveProductLog> context)
        {

            return context.Compensated();
        }
    }
}

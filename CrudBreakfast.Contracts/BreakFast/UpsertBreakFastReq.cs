using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudBreakfast.Contracts.BreakFast
{
	public record UpsertBreakFastReq(
		string Name,
		string Description,
		DateTime StartDateTime,
		DateTime EndDateTime,
		List<string> Savory,
		List<string> Sweet
	);
}
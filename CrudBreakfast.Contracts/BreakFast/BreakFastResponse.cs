using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudBreakfast.Contracts.BreakFast
{
	public record BreakFastResponse(
		Guid Id,
		string Name,
		string Description,
		DateTime StartDateTime,
		DateTime EndDateTime,
		DateTime LastModifiedDateTime,
		List<string> Savory,
		List<string> Sweet
	);
}
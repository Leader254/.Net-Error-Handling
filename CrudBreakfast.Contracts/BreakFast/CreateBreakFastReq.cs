namespace CrudBreakfast.Contracts.BreakFast
{
	public record CreateBreakFastReq(
		string Name,
		string Description,
		DateTime StartDateTime,
		DateTime EndDateTime,
		List<string> Savory,
		List<string> Sweet
	);
}
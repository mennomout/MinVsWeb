using Microsoft.AspNetCore.Mvc;

namespace Data;

public record QuoteModel([FromBody] string Quote);

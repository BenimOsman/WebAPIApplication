using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiApp.Model;

namespace CalculatorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly CalculationContext _context;

        public CalculationController(CalculationContext context)
        {
            _context = context;
        }

        // GET: api/Calculation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculation>>> GetCalculations()
        {
            return await _context.Calculations.ToListAsync();
        }

        // GET: api/Calculation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calculation>> GetCalculation(int id)
        {
            var calculation = await _context.Calculations.FindAsync(id);

            if (calculation == null)
            {
                return NotFound();
            }

            return calculation;
        }

        // PUT: api/Calculation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalculation(int id, Calculation calculation)
        {
            if (id != calculation.Id)
            {
                return BadRequest();
            }

            _context.Entry(calculation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Calculation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public ActionResult<Calculation> PostCalculation([FromBody] Calculation request)
        {
            double result;

            switch (request.Operator)
            {
                case "+":
                    result = request.Num1 + request.Num2;
                    break;
                case "-":
                    result = request.Num1 - request.Num2;
                    break;
                case "*":
                    result = request.Num1 * request.Num2;
                    break;
                case "/":
                    if (request.Num2 == 0)
                        return BadRequest("Division by zero is not allowed.");
                    result = request.Num1 / request.Num2;
                    break;
                default:
                    return BadRequest("Unsupported operator.");
            }

            // return Ok(new Calculation { Result = result });
            var calc = new Calculation
            {
                Num1 = request.Num1,
                Num2 = request.Num2,
                Operator = request.Operator,
                Result = result
            };
            _context.Add(calc);
            _context.SaveChanges();
            return CreatedAtAction("GetCalculation", new { id = calc.Id }, calc);

        }


        /*
            [HttpPost]
                public async Task<ActionResult<Calculation>> PostCalculation(Calculation calculation)
                {
                    _context.Calculations.Add(calculation);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetCalculation", new { id = calculation.Id }, calculation);
                }
        */
        // DELETE: api/Calculation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalculation(int id)
        {
            var calculation = await _context.Calculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            _context.Calculations.Remove(calculation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalculationExists(int id)
        {
            return _context.Calculations.Any(e => e.Id == id);
        }
    }
}
using BodyProgress.Server.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BodyProgress.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyTasksController : ControllerBase
    {
        private static List<DailyTask> _tasks = new List<DailyTask> 
        {
            new DailyTask {Id=1, Date=DateTime.Now, NutritionCompleted=true, CardioCompleted=true, StrengthTrainingCompleted=true},
            new DailyTask {Id=2, Date=DateTime.Now, NutritionCompleted=true, CardioCompleted=false, StrengthTrainingCompleted=true},
            new DailyTask {Id=3, Date=DateTime.Now, NutritionCompleted=false, CardioCompleted=true, StrengthTrainingCompleted=false}
        };

        // GET: api/<DailyTasksController>
        [HttpGet]
        public ActionResult<IEnumerable<DailyTask>> Get()
        {
            return Ok(_tasks);
        }

        // GET api/<DailyTasksController>/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<DailyTask>> Get(int id)
        {
            //FirstOrDefault liefert entweder den Wert oder null. Perfekt für nullchecks
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if(task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/<DailyTasksController>
        [HttpPost]
        public ActionResult<DailyTask> Post([FromBody] DailyTask newTask)
        {
            if(_tasks.Any(t => t.Id == newTask.Id))
            {
                return  BadRequest("Ein Task mit der Id existiert bereits");
            }

            _tasks.Add(newTask);
            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        // PUT api/<DailyTasksController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DailyTask updatedTask)
        {
            var existingDailyTask = _tasks.FirstOrDefault(t =>t.Id == id);
            if(existingDailyTask == null)
            {
                return NotFound();
            }

            existingDailyTask.CardioCompleted = updatedTask.CardioCompleted;
            existingDailyTask.NutritionCompleted = updatedTask.NutritionCompleted;
            existingDailyTask.StrengthTrainingCompleted = updatedTask.StrengthTrainingCompleted;

            return NoContent();
        }

        // DELETE api/<DailyTasksController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var taskToDelete = _tasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete == null)
            {
                return NotFound(new {message = "DailyTask nicht gefunden"});
            }

            _tasks.Remove(taskToDelete);
            return NoContent();
        }
    }
}

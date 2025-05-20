using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Task;

namespace Application.Interfaces
{
    public interface ITaskService
    {
        Task<ApiResponseDto<IEnumerable<TaskDto>>> GetAllTasks();
        Task<ApiResponseDto<TaskDto>> CreatedTask(TaskCreateDto taskCreatedDto);
        Task<ApiResponseDto<TaskDto>> UpdateTask(int id, TaskUpdateDto taskUpdateDto);
        Task<ApiResponseDto<IEnumerable<TaskDto>>> SearchTask(string searchName);
        Task<ApiResponseDto<bool>> DeleteTask(int id);
    }
}

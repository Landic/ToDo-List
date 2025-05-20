using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Common;
using Application.DTOs.Task;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork uof;
        private readonly IMapper mapper;

        public TaskService(IUnitOfWork uof, IMapper mapper)
        {
            this.uof = uof;
            this.mapper = mapper;
        }
        public async Task<ApiResponseDto<TaskDto>> CreatedTask(TaskCreateDto taskCreatedDto)
        {
            try
            {
                var task = mapper.Map<ToDoTask>(taskCreatedDto);
                await uof.Tasks.AddAsync(task);
                await uof.CompleteAsync();
                var taskDto = mapper.Map<TaskDto>(task);
                return ApiResponseDto<TaskDto>.SuccessResult(taskDto, "Task created succesfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<TaskDto>.FailureResult("Error creating task", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<bool>> DeleteTask(int id)
        {
            try
            {
                var task = await uof.Tasks.GetById(id);
                if (task == null)
                    return ApiResponseDto<bool>.FailureResult($"Task with ID {id} not found");
                uof.Tasks.Delete(task);
                await uof.CompleteAsync();
                return ApiResponseDto<bool>.SuccessResult(true, "Task deleted");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<bool>.FailureResult("Error deleting task", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TaskDto>>> GetAllTasks()
        {
            try
            {
                var tasks = await uof.Tasks.GetAllAsync();
                var taskDto = mapper.Map<IEnumerable<TaskDto>>(tasks);
                return ApiResponseDto<IEnumerable<TaskDto>>.SuccessResult(taskDto);
            }
            catch (Exception ex)
            {
                return ApiResponseDto<IEnumerable<TaskDto>>.FailureResult("Error retrieving task", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<IEnumerable<TaskDto>>> SearchTask(string searchName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchName))
                {
                    var tasks = await GetAllTasks();
                    return tasks;
                }
                searchName = searchName.ToLower();
                var searchTasks = await uof.Tasks.FindAsync(i => i.Name.ToLower().Contains(searchName));
                var taskDtos = mapper.Map<IEnumerable<TaskDto>>(searchTasks);
                return ApiResponseDto<IEnumerable<TaskDto>>.SuccessResult(taskDtos);
            }
            catch(Exception ex)
            {
                return ApiResponseDto<IEnumerable<TaskDto>>.FailureResult("Error searching task", new List<string> { ex.Message });
            }
        }

        public async Task<ApiResponseDto<TaskDto>> UpdateTask(int id, TaskUpdateDto taskUpdateDto)
        {
            try
            {
                var task = await uof.Tasks.GetById(id);
                if (task == null)
                    return ApiResponseDto<TaskDto>.FailureResult($"Task with ID {id} not found");
                if(taskUpdateDto.Name != null)
                    task.Name = taskUpdateDto.Name;
                if(taskUpdateDto.Description != null)
                    task.Description = taskUpdateDto.Description;
                if(taskUpdateDto.Deadline != null)
                    task.Deadline = taskUpdateDto.Deadline;
                uof.Tasks.Update(task);
                await uof.CompleteAsync();
                var taskDto = mapper.Map<TaskDto>(task);
                return ApiResponseDto<TaskDto>.SuccessResult(taskDto, "Task update successfully");
            }
            catch (Exception ex)
            {
                return ApiResponseDto<TaskDto>.FailureResult("Error updating task", new List<string> { ex.Message });
            }
        }
    }
}

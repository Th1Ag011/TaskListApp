using Mapster;
using TaskListApp.Aplication.Dtos.Request;
using TaskListApp.Aplication.Dtos.Response;
using TaskListApp.Aplication.Interfaces;
using TaskListApp.Domain.Entities;
using TaskListApp.Domain.Enums;
using TaskListApp.Domain.Interfaces;

namespace TaskListApp.Aplication.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemService(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        public async Task<TaskItemResponseDto> GetByIdAsync(Guid id)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);
            var taskItemResponseDto = taskItem.Adapt<TaskItemResponseDto>();
            return taskItemResponseDto;

        }

        public async Task<IEnumerable<TaskItemResponseDto>> GetAllAsync()
        {
            var tasks = await _taskItemRepository.GetAllAsync();
            var taskItemResponseDto = tasks.Adapt<IEnumerable<TaskItemResponseDto>>();
            return taskItemResponseDto;
        }

        public async Task<IEnumerable<TaskItemResponseDto>> GetAllByStatus(Status status)
        {
            var taskItemResponseDto = await GetAllAsync();
            if (!taskItemResponseDto.Any())
            {
                return taskItemResponseDto;
            }

            return taskItemResponseDto.Where(t => t.Status == status).ToList();
        }

        public async Task <Guid> AddAsync(TaskItemRequestDto taskItemRequestDto)
        {
            var taskItem = new TaskItem(
                taskItemRequestDto.Name,
                taskItemRequestDto.Description,
                taskItemRequestDto.Status,
                taskItemRequestDto.Priority,
                taskItemRequestDto.DueDate
            );

            await _taskItemRepository.AddAsync(taskItem);
            await _taskItemRepository.SaveAsync();
            return taskItem.Id;
        }

        public async Task <bool> UpdateAsync(Guid id, TaskItemRequestDto taskItemRequestDto)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return false;
            }

            taskItem.Update(
                taskItemRequestDto.Name,
                taskItemRequestDto.Description,
                taskItemRequestDto.Status,
                taskItemRequestDto.Priority,
                taskItemRequestDto.DueDate
            );

            _taskItemRepository.Update(taskItem);
            await _taskItemRepository.SaveAsync();
            return true;
        }

        public async Task <bool> DeleteAsync(Guid id)
        {
            var taskItem = await _taskItemRepository.GetByIdAsync(id);
            if (taskItem == null)
            {
                return false;
            }

            _taskItemRepository.Delete(taskItem);
            await _taskItemRepository.SaveAsync();
            return true;
        }
    }
}

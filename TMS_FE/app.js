// API Base Configuration
const API_BASE_URL = 'http://localhost:5149';

// Shared Layout Template Builder
function renderLayout(activePage) {
    const sidebarContainer = document.getElementById('layout-sidebar');
    const overlayContainer = document.getElementById('layout-overlay');

    if (sidebarContainer) {
        sidebarContainer.innerHTML = `
            <div class="logo-container">
                <div class="logo-icon">T</div>
                <div class="logo-text">TaskFlow</div>
            </div>
            <nav class="sidebar-nav">
                <a href="dashboard.html" class="nav-link ${activePage === 'dashboard' ? 'active' : ''}" data-page="dashboard">
                    <svg fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6a2 2 0 012-2h2a2 2 0 012 2v4a2 2 0 01-2 2H6a2 2 0 01-2-2V6zM14 6a2 2 0 012-2h2a2 2 0 012 2v4a2 2 0 01-2 2h-2a2 2 0 01-2-2V6zM4 16a2 2 0 012-2h2a2 2 0 012 2v4a2 2 0 01-2 2H6a2 2 0 01-2-2v-4zM14 16a2 2 0 012-2h2a2 2 0 012 2v4a2 2 0 01-2 2h-2a2 2 0 01-2-2v-4z"></path>
                    </svg>
                    Dashboard
                </a>
                <a href="tasks.html" class="nav-link ${activePage === 'tasks' ? 'active' : ''}" data-page="tasks">
                    <svg fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 5H7a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2m-6 9l2 2 4-4"></path>
                    </svg>
                    Tasks
                </a>
                <a href="users.html" class="nav-link ${activePage === 'users' ? 'active' : ''}" data-page="users">
                    <svg fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z"></path>
                    </svg>
                    Users
                </a>
                <hr style="border: none; border-top: 1px solid var(--border-color); margin: 1rem 0;" />
                <a href="add-task.html" class="nav-link ${activePage === 'add-task' ? 'active' : ''}">
                    <svg fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 4v16m8-8H4"></path>
                    </svg>
                    Add Task
                </a>
            </nav>
        `;
    }

    if (overlayContainer) {
        overlayContainer.className = 'sidebar-overlay';
        overlayContainer.addEventListener('click', toggleSidebar);
    }

    // Add Mobile Menu Toggle Button to DOM
    const header = document.querySelector('.page-header');
    if (header) {
        const toggleBtn = document.createElement('button');
        toggleBtn.className = 'menu-toggle';
        toggleBtn.innerHTML = `
            <svg style="width: 24px; height: 24px;" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"></path>
            </svg>
        `;
        toggleBtn.addEventListener('click', toggleSidebar);
        document.body.appendChild(toggleBtn);
    }
}

function toggleSidebar() {
    const sidebar = document.querySelector('.sidebar');
    const overlay = document.querySelector('.sidebar-overlay');
    if (sidebar && overlay) {
        sidebar.classList.toggle('active');
        overlay.classList.toggle('active');
    }
}

// Custom Toast System
function showToast(type, title, message, errors = []) {
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        container.className = 'toast-container';
        document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;

    let iconSVG = '';
    if (type === 'success') {
        iconSVG = `
            <svg class="toast-icon" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"></path>
            </svg>
        `;
    } else {
        iconSVG = `
            <svg class="toast-icon" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"></path>
            </svg>
        `;
    }

    let errorsHTML = '';
    if (errors && errors.length > 0) {
        errorsHTML = `
            <ul class="toast-errors-list">
                ${errors.map(err => `<li>${escapeHtml(err)}</li>`).join('')}
            </ul>
        `;
    }

    toast.innerHTML = `
        ${iconSVG}
        <div class="toast-content">
            <div class="toast-title">${escapeHtml(title)}</div>
            <div class="toast-message">${escapeHtml(message)}</div>
            ${errorsHTML}
        </div>
        <button class="toast-close">
            <svg style="width: 16px; height: 16px;" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
            </svg>
        </button>
    `;

    toast.querySelector('.toast-close').addEventListener('click', () => {
        toast.style.animation = 'slideIn 0.3s cubic-bezier(0.16, 1, 0.3, 1) reverse';
        setTimeout(() => toast.remove(), 300);
    });

    container.appendChild(toast);

    // Auto-remove after 5 seconds
    setTimeout(() => {
        if (toast.parentElement) {
            toast.style.animation = 'slideIn 0.3s cubic-bezier(0.16, 1, 0.3, 1) reverse';
            setTimeout(() => toast.remove(), 300);
        }
    }, 5000);
}

// Custom Confirmation Dialog Modal
function showConfirmModal(title, message, onConfirm) {
    let overlay = document.getElementById('confirm-modal-overlay');
    if (!overlay) {
        overlay = document.createElement('div');
        overlay.id = 'confirm-modal-overlay';
        overlay.className = 'modal-overlay';
        document.body.appendChild(overlay);
    }

    overlay.innerHTML = `
        <div class="modal-card">
            <div class="modal-header">
                <svg fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"></path>
                </svg>
                <h3>${escapeHtml(title)}</h3>
            </div>
            <div class="modal-body">
                <p>${escapeHtml(message)}</p>
            </div>
            <div class="modal-actions">
                <button class="btn btn-secondary" id="modal-cancel-btn">Cancel</button>
                <button class="btn btn-danger" id="modal-confirm-btn">Confirm Delete</button>
            </div>
        </div>
    `;

    const cancelBtn = overlay.querySelector('#modal-cancel-btn');
    const confirmBtn = overlay.querySelector('#modal-confirm-btn');

    const closeModal = () => {
        overlay.classList.remove('active');
        setTimeout(() => {
            overlay.innerHTML = '';
        }, 300);
    };

    cancelBtn.addEventListener('click', closeModal);

    confirmBtn.addEventListener('click', () => {
        onConfirm();
        closeModal();
    });

    // Activate animation
    setTimeout(() => {
        overlay.classList.add('active');
    }, 10);
}

// Mock Database Seed Keys
const MOCK_USERS_KEY = 'taskflow_mock_users';
const MOCK_TASKS_KEY = 'taskflow_mock_tasks';

function initMockDb() {
    if (!localStorage.getItem(MOCK_USERS_KEY)) {
        const defaultUsers = [
            { userId: 1, userName: 'Abdul Baasith', email: 'abdul@example.com' },
            { userId: 2, userName: 'Student One', email: 'student1@example.com' },
            { userId: 3, userName: 'Student Two', email: 'student2@example.com' }
        ];
        localStorage.setItem(MOCK_USERS_KEY, JSON.stringify(defaultUsers));
    }
    if (!localStorage.getItem(MOCK_TASKS_KEY)) {
        const defaultTasks = [
            { taskId: 1, title: 'Create database', description: 'Create Users and Tasks tables', status: 'Done', createdDate: '2026-06-15T10:30:00', userId: 1 },
            { taskId: 2, title: 'Build user API', description: 'Create user endpoints', status: 'In Progress', createdDate: '2026-06-15T11:00:00', userId: 1 },
            { taskId: 3, title: 'Create frontend', description: 'Build HTML pages', status: 'Todo', createdDate: '2026-06-15T11:30:00', userId: 2 },
            { taskId: 4, title: 'Test API', description: 'Use Postman to test endpoints', status: 'Todo', createdDate: '2026-06-15T12:00:00', userId: 3 }
        ];
        localStorage.setItem(MOCK_TASKS_KEY, JSON.stringify(defaultTasks));
    }
}

function handleMockRequest(endpoint, method, bodyStr) {
    initMockDb();
    const users = JSON.parse(localStorage.getItem(MOCK_USERS_KEY) || '[]');
    const tasks = JSON.parse(localStorage.getItem(MOCK_TASKS_KEY) || '[]');

    const getUserName = (userId) => {
        const u = users.find(x => x.userId === parseInt(userId));
        return u ? u.userName : 'Unknown';
    };

    const [path, queryStr] = endpoint.split('?');
    const queryParams = new URLSearchParams(queryStr || '');
    let cleanPath = path.replace(/\/$/, "");

    // GET /api/tasks/search
    if (cleanPath === '/api/tasks/search') {
        const searchName = (queryParams.get('name') || '').toLowerCase();
        const filteredTasks = tasks
            .filter(t => t.title.toLowerCase().includes(searchName))
            .map(t => ({ ...t, userName: getUserName(t.userId) }));
        return { success: true, message: "Tasks search completed.", data: filteredTasks, errors: [] };
    }

    const taskByIdMatch = cleanPath.match(/^\/api\/tasks\/(\d+)$/);
    
    // GET /api/tasks/{id}
    if (taskByIdMatch && method === 'GET') {
        const id = parseInt(taskByIdMatch[1]);
        const task = tasks.find(t => t.taskId === id);
        if (!task) return { success: false, message: "Task not found.", data: null, errors: ["Task does not exist."] };
        return { success: true, message: "Task loaded.", data: { ...task, userName: getUserName(task.userId) }, errors: [] };
    }

    const taskStatusMatch = cleanPath.match(/^\/api\/tasks\/(\d+)\/status$/);
    
    // PUT /api/tasks/{id}/status
    if (taskStatusMatch && method === 'PUT') {
        const id = parseInt(taskStatusMatch[1]);
        const body = JSON.parse(bodyStr || '{}');
        const taskIdx = tasks.findIndex(t => t.taskId === id);
        if (taskIdx === -1) return { success: false, message: "Task not found.", data: null, errors: ["Task does not exist."] };
        
        const validStatuses = ['Todo', 'In Progress', 'Done'];
        if (!validStatuses.includes(body.status)) {
            return { success: false, message: "Validation failed.", data: null, errors: ["Status must be Todo, In Progress, or Done."] };
        }
        
        tasks[taskIdx].status = body.status;
        localStorage.setItem(MOCK_TASKS_KEY, JSON.stringify(tasks));
        return { success: true, message: "Status updated.", data: tasks[taskIdx], errors: [] };
    }

    // PUT /api/tasks/{id}
    if (taskByIdMatch && method === 'PUT') {
        const id = parseInt(taskByIdMatch[1]);
        const body = JSON.parse(bodyStr || '{}');
        const taskIdx = tasks.findIndex(t => t.taskId === id);
        if (taskIdx === -1) return { success: false, message: "Task not found.", data: null, errors: ["Task does not exist."] };

        const errors = [];
        if (!body.title) errors.push("Title is required.");
        if (body.title && body.title.length > 200) errors.push("Title is too long (max 200 chars).");
        if (body.description && body.description.length > 500) errors.push("Description is too long (max 500 chars).");
        if (!body.userId) errors.push("UserId is required.");
        const userExists = users.some(u => u.userId === parseInt(body.userId));
        if (!userExists) errors.push("Selected user does not exist.");

        if (errors.length > 0) {
            return { success: false, message: "Validation failed.", data: null, errors };
        }

        tasks[taskIdx].title = body.title;
        tasks[taskIdx].description = body.description || null;
        tasks[taskIdx].status = body.status || 'Todo';
        tasks[taskIdx].userId = parseInt(body.userId);
        localStorage.setItem(MOCK_TASKS_KEY, JSON.stringify(tasks));
        
        return { success: true, message: "Task updated successfully.", data: tasks[taskIdx], errors: [] };
    }

    // DELETE /api/tasks/{id}
    if (taskByIdMatch && method === 'DELETE') {
        const id = parseInt(taskByIdMatch[1]);
        const taskIdx = tasks.findIndex(t => t.taskId === id);
        if (taskIdx === -1) return { success: false, message: "Task not found.", data: null, errors: ["Task does not exist."] };
        
        tasks.splice(taskIdx, 1);
        localStorage.setItem(MOCK_TASKS_KEY, JSON.stringify(tasks));
        return { success: true, message: "Task deleted successfully.", data: null, errors: [] };
    }

    // GET /api/tasks
    if (cleanPath === '/api/tasks' && method === 'GET') {
        const joinedTasks = tasks.map(t => ({ ...t, userName: getUserName(t.userId) }));
        return { success: true, message: "Tasks loaded.", data: joinedTasks, errors: [] };
    }

    // POST /api/tasks
    if (cleanPath === '/api/tasks' && method === 'POST') {
        const body = JSON.parse(bodyStr || '{}');
        const errors = [];
        if (!body.title) errors.push("Title is required.");
        if (body.title && body.title.length > 200) errors.push("Title is too long (max 200 chars).");
        if (body.description && body.description.length > 500) errors.push("Description is too long (max 500 chars).");
        if (!body.userId) errors.push("UserId is required.");
        const userExists = users.some(u => u.userId === parseInt(body.userId));
        if (!userExists) errors.push("Selected user does not exist.");

        if (errors.length > 0) {
            return { success: false, message: "Validation failed.", data: null, errors };
        }

        const newId = tasks.length > 0 ? Math.max(...tasks.map(t => t.taskId)) + 1 : 1;
        const newTask = {
            taskId: newId,
            title: body.title,
            description: body.description || null,
            status: body.status || 'Todo',
            createdDate: new Date().toISOString(),
            userId: parseInt(body.userId)
        };

        tasks.push(newTask);
        localStorage.setItem(MOCK_TASKS_KEY, JSON.stringify(tasks));
        return { success: true, message: "Task created successfully.", data: { ...newTask, userName: getUserName(newTask.userId) }, errors: [] };
    }

    const userTasksMatch = cleanPath.match(/^\/api\/users\/(\d+)\/tasks$/);
    
    // GET /api/users/{id}/tasks (LEFT JOIN simulation)
    if (userTasksMatch && method === 'GET') {
        const id = parseInt(userTasksMatch[1]);
        const user = users.find(u => u.userId === id);
        if (!user) return { success: false, message: "User not found.", data: null, errors: ["Selected user does not exist."] };
        
        const userTasks = tasks
            .filter(t => t.userId === id)
            .map(t => ({ ...t, userName: user.userName }));

        return { 
            success: true, 
            message: "User tasks loaded.", 
            data: {
                userId: user.userId,
                userName: user.userName,
                email: user.email,
                tasks: userTasks
            }, 
            errors: [] 
        };
    }

    const userByIdMatch = cleanPath.match(/^\/api\/users\/(\d+)$/);
    
    // GET /api/users/{id}
    if (userByIdMatch && method === 'GET') {
        const id = parseInt(userByIdMatch[1]);
        const user = users.find(u => u.userId === id);
        if (!user) return { success: false, message: "User not found.", data: null, errors: ["User does not exist."] };
        return { success: true, message: "User loaded.", data: user, errors: [] };
    }

    // GET /api/users
    if (cleanPath === '/api/users' && method === 'GET') {
        return { success: true, message: "Users loaded.", data: users, errors: [] };
    }

    // POST /api/users
    if (cleanPath === '/api/users' && method === 'POST') {
        const body = JSON.parse(bodyStr || '{}');
        const errors = [];
        if (!body.userName) errors.push("UserName is required.");
        if (body.userName && body.userName.length > 100) errors.push("UserName cannot exceed 100 characters.");
        if (!body.email) errors.push("Email is required.");
        if (body.email && body.email.length > 100) errors.push("Email cannot exceed 100 characters.");
        
        const emailExists = users.some(u => u.email.toLowerCase() === (body.email || '').toLowerCase());
        if (emailExists) errors.push("Duplicate email address: This email is already registered.");

        if (errors.length > 0) {
            return { success: false, message: "Validation failed.", data: null, errors };
        }

        const newId = users.length > 0 ? Math.max(...users.map(u => u.userId)) + 1 : 1;
        const newUser = {
            userId: newId,
            userName: body.userName,
            email: body.email
        };

        users.push(newUser);
        localStorage.setItem(MOCK_USERS_KEY, JSON.stringify(users));
        return { success: true, message: "User created successfully.", data: newUser, errors: [] };
    }

    return { success: false, message: "Mock route not implemented.", data: null, errors: [] };
}

function showMockBanner() {
    if (document.getElementById('mock-mode-banner')) return;
    const banner = document.createElement('div');
    banner.id = 'mock-mode-banner';
    banner.style.cssText = `
        position: fixed;
        bottom: 1rem;
        left: 1rem;
        background: var(--warning-bg);
        border: 1px solid var(--warning-border);
        color: var(--warning);
        padding: 0.5rem 1rem;
        border-radius: var(--radius-md);
        font-size: 0.75rem;
        font-weight: 600;
        z-index: 10000;
        backdrop-filter: blur(8px);
        display: flex;
        align-items: center;
        gap: 0.5rem;
        box-shadow: 0 4px 20px rgba(0,0,0,0.3);
    `;
    banner.innerHTML = `
        <span class="loading-spinner" style="width: 10px; height: 10px; border-width: 1px; border-top-color: var(--warning);"></span>
        Backend Offline: Running in client-side Sandbox
    `;
    document.body.appendChild(banner);
}

// Fetch API Wrapper with Standardized Error Handling and Loader state
async function apiFetch(endpoint, options = {}) {
    const url = `${API_BASE_URL}${endpoint}`;
    
    // Default headers
    if (!options.headers) {
        options.headers = {};
    }
    if (!(options.body instanceof FormData) && !options.headers['Content-Type']) {
        options.headers['Content-Type'] = 'application/json';
    }

    // Handle button loader states
    let submitBtn = null;
    let originalBtnHTML = '';
    if (options.submitButton) {
        submitBtn = options.submitButton;
        originalBtnHTML = submitBtn.innerHTML;
        submitBtn.disabled = true;
        submitBtn.innerHTML = '<span class="loading-spinner"></span> Processing...';
    }

    try {
        const response = await fetch(url, {
            method: options.method || 'GET',
            headers: options.headers,
            body: options.body
        });

        // Parse standardized response format
        let result = null;
        const responseText = await response.text();
        
        try {
            if (responseText) {
                result = JSON.parse(responseText);
            }
        } catch (e) {
            console.error('Error parsing JSON response', e);
        }

        if (!response.ok) {
            // Handle HTTP error responses
            const errorMsg = result && result.message ? result.message : `API responded with status code ${response.status}`;
            const errorList = result && result.errors ? result.errors : [];
            
            showToast('error', 'Request Failed', errorMsg, errorList);
            return null;
        }

        // Check internal success flag of API Response standard
        if (result && result.success === false) {
            showToast('error', 'Action Incomplete', result.message || 'Operation failed', result.errors || []);
            return null;
        }

        return result;
    } catch (error) {
        console.warn('Network error: falling back to client-side Mock API handler', error);
        
        // Show offline sandboxed mode warning toast/banner
        showMockBanner();

        // Simulate network delay for a rich user experience
        await new Promise(resolve => setTimeout(resolve, 300));
        
        // Execute mock route
        const mockResult = handleMockRequest(endpoint, options.method || 'GET', options.body);
        
        if (!mockResult.success) {
            showToast('error', 'Validation Failure (Sandbox)', mockResult.message || 'Operation failed', mockResult.errors || []);
            return null;
        }
        
        return mockResult;
    } finally {
        // Restore button state
        if (submitBtn) {
            submitBtn.disabled = false;
            submitBtn.innerHTML = originalBtnHTML;
        }
    }
}

// Utility Helpers
function escapeHtml(unsafe) {
    if (!unsafe) return '';
    return String(unsafe)
         .replace(/&/g, "&amp;")
         .replace(/</g, "&lt;")
         .replace(/>/g, "&gt;")
         .replace(/"/g, "&quot;")
         .replace(/'/g, "&#039;");
}

function getQueryParam(name) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(name);
}

function formatDate(isoString) {
    if (!isoString) return '-';
    try {
        const date = new Date(isoString);
        return date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    } catch (e) {
        return isoString;
    }
}

// Input validator matching Field-Level Validation specs
function validateField(id, ruleFn, errorMsgId, message) {
    const input = document.getElementById(id);
    const errorEl = document.getElementById(errorMsgId);
    
    if (!input || !errorEl) return true;

    const val = input.value.trim();
    const isValid = ruleFn(val);
    
    if (!isValid) {
        errorEl.textContent = message;
        errorEl.style.display = 'block';
        input.style.borderColor = 'var(--danger)';
        return false;
    } else {
        errorEl.style.display = 'none';
        input.style.borderColor = '';
        return true;
    }
}

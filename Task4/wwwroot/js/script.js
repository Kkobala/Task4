document.addEventListener("DOMContentLoaded", function () {
    const selectAllCheckbox = document.getElementById("selectAll");
    const checkboxes = document.querySelectorAll(".checkbox-item");
    const blockButton = document.getElementById("blockButton");
    const unblockButton = document.getElementById("unblockButton");
    const deleteButton = document.getElementById("deleteButton");

    selectAllCheckbox.addEventListener("change", () => {
        checkboxes.forEach((checkbox) => {
            checkbox.checked = selectAllCheckbox.checked;
        });
    });

    blockButton.addEventListener("click", () => {
        const selectedUserIds = getSelectedUserIds();
        selectedUserIds.forEach((userId) => {
            fetch(`/api/Login/block-user?id=${userId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    console.log("API response:", data);
                })
                .catch(error => {
                    console.error("API error:", error);
                });
        });
    });

    unblockButton.addEventListener("click", () => {
        const selectedUserIds = getSelectedUserIds();
        selectedUserIds.forEach((userId) => {
            fetch(`/api/Login/unblock-user?id=${userId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    console.log("API response:", data);
                })
                .catch(error => {
                    console.error("API error:", error);
                });
        });
    });

    deleteButton.addEventListener("click", () => {
        const selectedUserIds = getSelectedUserIds();
        selectedUserIds.forEach((userId) => {
            fetch(`/api/Login/delete-user?id=${userId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => response.json())
                .then(data => {
                    console.log("API response:", data);
                })
                .catch(error => {
                    console.error("API error:", error);
                });
        });
    });

    function getSelectedUserIds() {
        const selectedUserIds = [];
        checkboxes.forEach((checkbox) => {
            if (checkbox.checked) {
                selectedUserIds.push(checkbox.getAttribute("data-user-id"));
            }
        });
        return selectedUserIds;
    }
});

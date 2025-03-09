const apiUrl = "https://localhost:7298/api/HangHoa"; // Thay 7298 bằng port thực tế

let isEdit = false; // Biến để xác định thêm hay sửa

window.onload = () => loadHangHoa();

function loadHangHoa() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            console.log("Dữ liệu từ API:", data); // Debug
            const tbody = document.getElementById("hangHoaList");
            tbody.innerHTML = "";
            if (Array.isArray(data)) {
                data.forEach(item => {
                    const row = `<tr>
                        <td>${item.MaHangHoa || "Không có"}</td>
                        <td>${item.TenHangHoa || "Không có"}</td>
                        <td>${item.SoLuong || "Không có"}</td>
                        <td>${item.GhiChu || ""}</td>
                        <td>
                            <button onclick="editHangHoa('${item.MaHangHoa}')">Sửa</button>
                            <button onclick="deleteHangHoa('${item.MaHangHoa}')">Xóa</button>
                            <button onclick="updateGhiChu('${item.MaHangHoa}')">Cập nhật Ghi Chú</button>
                        </td>
                    </tr>`;
                    tbody.innerHTML += row;
                });
            } else {
                console.error("Dữ liệu không phải là mảng:", data);
            }
        })
        .catch(error => console.error("Error:", error));
}

function createOrUpdateHangHoa() {
    const maHangHoa = document.getElementById("maHangHoa").value;
    const tenHangHoa = document.getElementById("tenHangHoa").value;
    const soLuong = document.getElementById("soLuong").value;
    const ghiChu = document.getElementById("ghiChu").value;

    if (!tenHangHoa) {
        alert("Tên hàng hóa không được để trống!");
        return;
    }
    if (maHangHoa.length !== 9) {
        alert("Mã hàng hóa phải đúng 9 ký tự!");
        return;
    }

    const hangHoa = { MaHangHoa: maHangHoa, TenHangHoa: tenHangHoa, SoLuong: parseInt(soLuong), GhiChu: ghiChu };
    const method = isEdit ? "PUT" : "POST";
    const url = isEdit ? `${apiUrl}/${maHangHoa}` : apiUrl;

    fetch(url, {
        method: method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(hangHoa)
    })
        .then(response => {
            if (response.ok) {
                loadHangHoa();
                clearForm();
                isEdit = false;
            } else {
                response.text().then(text => alert(`Lỗi khi lưu! Mã lỗi: ${response.status}. Chi tiết: ${text}`));
            }
        })
        .catch(error => console.error("Error:", error));
}

function editHangHoa(maHangHoa) {
    fetch(`${apiUrl}/${maHangHoa}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Lỗi khi lấy dữ liệu hàng hóa: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("maHangHoa").value = data.MaHangHoa;
            document.getElementById("tenHangHoa").value = data.TenHangHoa;
            document.getElementById("soLuong").value = data.SoLuong;
            document.getElementById("ghiChu").value = data.GhiChu || "";
            isEdit = true; // Đảm bảo đặt đúng
        })
        .catch(error => {
            console.error("Error:", error);
            alert("Không thể lấy dữ liệu hàng hóa để sửa!");
        });
}

function deleteHangHoa(maHangHoa) {
    if (confirm("Bạn có chắc muốn xóa?")) {
        fetch(`${apiUrl}/${maHangHoa}`, { method: "DELETE" })
            .then(response => {
                if (response.ok) loadHangHoa();
            })
            .catch(error => console.error("Error:", error));
    }
}

function updateGhiChu(maHangHoa) {
    const ghiChu = prompt("Nhập ghi chú mới:");
    if (ghiChu !== null) {
        fetch(`${apiUrl}/${maHangHoa}/update-note`, {
            method: "PATCH",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(ghiChu)
        })
            .then(response => {
                if (response.ok) loadHangHoa();
            })
            .catch(error => console.error("Error:", error));
    }
}

function clearForm() {
    document.getElementById("maHangHoa").value = "";
    document.getElementById("tenHangHoa").value = "";
    document.getElementById("soLuong").value = "";
    document.getElementById("ghiChu").value = "";
}
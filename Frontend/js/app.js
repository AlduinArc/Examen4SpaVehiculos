const apiBase = "https://localhost:44307/api/autos";
const autoForm = document.getElementById("autoForm");
const autosList = document.getElementById("autosList");

let editando = null;

async function obtenerAutos() {
  try {
    const res = await fetch(`${apiBase}/consultar?id=0`);
    const data = await res.json();
    mostrarAutos(data);
  } catch (err) {
    console.error("Error cargando autos", err);
  }
}

function mostrarAutos(autos) {
  autosList.innerHTML = "";
  autos.forEach(auto => {
    const div = document.createElement("div");
    div.className = "p-3 border rounded flex justify-between items-center";

    div.innerHTML = `
      <div>
        <p class="font-semibold">${auto.marca} ${auto.modelo}</p>
        <p class="text-sm text-gray-500">${auto.placa}</p>
      </div>
      <div class="space-x-2">
        <button class="text-blue-600" onclick="editarAuto(${JSON.stringify(auto).replace(/"/g, '&quot;')})">Editar</button>
        <button class="text-red-600" onclick="eliminarAuto(${auto.idAuto})">Eliminar</button>
      </div>
    `;
    autosList.appendChild(div);
  });
}

autoForm.addEventListener("submit", async (e) => {
  e.preventDefault();

  const data = {
    marca: document.getElementById("marca").value,
    modelo: document.getElementById("modelo").value,
    placa: document.getElementById("placa").value,
  };

  if (editando) {
    data.idAuto = editando.idAuto;
    data.idCliente = editando.idCliente;
    await fetch(`${apiBase}/actualizar`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    editando = null;
  } else {
    await fetch(`${apiBase}/crear`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
  }

  autoForm.reset();
  obtenerAutos();
});

function editarAuto(auto) {
  document.getElementById("marca").value = auto.marca;
  document.getElementById("modelo").value = auto.modelo;
  document.getElementById("placa").value = auto.placa;
  editando = auto;
}

async function eliminarAuto(id) {
  if (confirm("¿Estás seguro de eliminar este auto?")) {
    await fetch(`${apiBase}/eliminar?id=${id}`, { method: "DELETE" });
    obtenerAutos();
  }
}

obtenerAutos();

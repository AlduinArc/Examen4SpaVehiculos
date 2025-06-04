const apiAutos = "http://spavehiculosproy.runasp.net/api/autos";
const apiClientes = "http://spavehiculosproy.runasp.net/api/clientes/listar";

const formAuto = document.getElementById("autoForm");
const autosList = document.getElementById("autosList");

let editandoAuto = null;
let clientesMap = {};

// Cargar clientes y llenar el select
async function cargarClientes() {
  const res = await fetch(apiClientes);
  const clientes = await res.json();
  const select = document.getElementById("idCliente");

  clientes.forEach(c => {
    const nombreCompleto = `${c.Nombre} ${c.Apellido}`;
    clientesMap[c.idCliente] = nombreCompleto;

    const option = document.createElement("option");
    option.value = c.idCliente;
    option.textContent = nombreCompleto;
    select.appendChild(option);
  });
}

// Obtener y mostrar autos
async function obtenerAutos() {
  const res = await fetch(apiAutos + "/listar");
  const autos = await res.json();
  mostrarAutos(autos);
}

// Mostrar autos en una tabla
function mostrarAutos(autos) {
  autosList.innerHTML = "";
  autos.forEach(auto => {
    const tr = document.createElement("tr");
    const clienteNombre = clientesMap[auto.idCliente] || `ID ${auto.idCliente}`;
    tr.innerHTML = `
      <td class="border p-2">${auto.idAuto}</td>
      <td class="border p-2">${auto.Marca}</td>
      <td class="border p-2">${auto.Modelo}</td>
      <td class="border p-2">${auto.Placa}</td>
      <td class="border p-2">${clienteNombre}</td>
      <td class="border p-2 space-x-2">
        <button class="text-blue-600" onclick='editarAuto(${JSON.stringify(auto).replace(/"/g, "&quot;")})'>Editar</button>
        <button class="text-red-600" onclick='eliminarAuto(${auto.idAuto})'>Eliminar</button>
      </td>
    `;
    autosList.appendChild(tr);
  });
}

// Guardar (crear o actualizar)
formAuto.addEventListener("submit", async e => {
  e.preventDefault();
  const data = {
    Marca: document.getElementById("Marca").value,
    Modelo: document.getElementById("Modelo").value,
    Placa: document.getElementById("Placa").value,
    idCliente: parseInt(document.getElementById("idCliente").value)
  };

  const url = editandoAuto ? `${apiAutos}/actualizar` : `${apiAutos}/crear`;
  const method = editandoAuto ? "PUT" : "POST";

  if (editandoAuto) data.idAuto = editandoAuto.idAuto;

  await fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  });

  formAuto.reset();
  editandoAuto = null;
  await obtenerAutos();
});

// Cargar datos al formulario para editar
function editarAuto(auto) {
  document.getElementById("idAuto").value = auto.idAuto;
  document.getElementById("Marca").value = auto.Marca;
  document.getElementById("Modelo").value = auto.Modelo;
  document.getElementById("Placa").value = auto.Placa;
  document.getElementById("idCliente").value = auto.idCliente;
  editandoAuto = auto;
}

// Eliminar auto
async function eliminarAuto(id) {
  if (confirm("¿Estás seguro de eliminar este auto?")) {
    await fetch(`${apiAutos}/eliminar?id=${id}`, { method: "DELETE" });
    await obtenerAutos();
  }
}

// Inicializar
(async function iniciar() {
  await cargarClientes();
  await obtenerAutos();
})();

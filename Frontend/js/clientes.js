const apiClientes = "http://spavehiculosproy.runasp.net/api/clientes";
const form = document.getElementById("cliente-form");
const tabla = document.getElementById("clientes-body");
let editandoCliente = null;

async function cargarClientes() {
  try {
    const res = await fetch(apiClientes + "/listar");
    const data = await res.json();

    tabla.innerHTML = data.map(c => `
      <tr>
        <td class="border p-2">${c.idCliente}</td>
        <td class="border p-2">${c.Nombre}</td>
        <td class="border p-2">${c.Apellido}</td>
        <td class="border p-2">${c.Telefono}</td>
        <td class="border p-2">
          <button onclick='editar(${JSON.stringify(c)})' class="text-blue-600">Editar</button>
          <button onclick='eliminar(${c.idCliente})' class="text-red-600 ml-2">Eliminar</button>
        </td>
      </tr>`).join("");
  } catch (error) {
    console.error("Error al cargar clientes:", error);
  }
}

form.addEventListener("submit", async e => {
  e.preventDefault();
  const data = {
    Nombre: document.getElementById("nombre").value,
    Apellido: document.getElementById("apellido").value,
    Telefono: document.getElementById("telefono").value
  };

  let url = apiClientes + "/crear";
  let method = "POST";

  if (editandoCliente) {
    data.idCliente = editandoCliente.idCliente;
    url = apiClientes + "/actualizar";
    method = "PUT";
  }

  await fetch(url, {
    method,
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  });

  form.reset();
  editandoCliente = null;
  cargarClientes();
});

function editar(cliente) {
  document.getElementById("nombre").value = cliente.Nombre;
  document.getElementById("apellido").value = cliente.Apellido;
  document.getElementById("telefono").value = cliente.Telefono;
  editandoCliente = cliente;
}

async function eliminar(id) {
  if (confirm("¿Eliminar cliente?")) {
    await fetch(apiClientes + "/eliminar?id=" + id, { method: "DELETE" });
    cargarClientes();
  }
}

cargarClientes();

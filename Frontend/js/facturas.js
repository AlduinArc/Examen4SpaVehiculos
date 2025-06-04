const apiBase = "http://spavehiculosproy.runasp.net/api/facturas";
const apiClientes = "http://spavehiculosproy.runasp.net/api/clientes/listar";
const apiEmpleados = "http://spavehiculosproy.runasp.net/api/empleados/listar";

const form = document.getElementById("factura-form");
const tbody = document.getElementById("facturas-body");
const selectCliente = document.getElementById("idCliente");
const selectEmpleado = document.getElementById("idEmpleado");

let editando = null;
let clientesMap = {};
let empleadosMap = {};

async function cargarClientes() {
  try {
    const res = await fetch(apiClientes);
    if (!res.ok) throw new Error(`Error en API de clientes: ${res.status}`);
    const clientes = await res.json();
    console.log("Clientes:", clientes);
    clientes.forEach(c => {
      if (c.idCliente && c.Nombre && c.Apellido) {
        clientesMap[c.idCliente] = `${c.Nombre} ${c.Apellido}`;
        const option = document.createElement("option");
        option.value = c.idCliente;
        option.textContent = `${c.Nombre} ${c.Apellido}`;
        selectCliente.appendChild(option);
      }
    });
  } catch (err) {
    console.error("Error al cargar clientes", err);
  }
}

async function cargarEmpleados() {
  try {
    const res = await fetch(apiEmpleados);
    if (!res.ok) throw new Error(`Error en API de empleados: ${res.status}`);
    const empleados = await res.json();
    console.log("Empleados:", empleados);
    empleados.forEach(e => {
      if (e.idEmpleado && e.Nombre && e.Apellido) {
        empleadosMap[e.idEmpleado] = `${e.Nombre} ${e.Apellido}`;
        const option = document.createElement("option");
        option.value = e.idEmpleado;
        option.textContent = `${e.Nombre} ${e.Apellido}`;
        selectEmpleado.appendChild(option);
      }
    });
  } catch (err) {
    console.error("Error al cargar empleados", err);
  }
}

async function obtenerFacturas() {
  try {
    const res = await fetch(`${apiBase}/listar`);
    if (!res.ok) throw new Error(`Error en API de facturas: ${res.status}`);
    const data = await res.json();
    console.log("Facturas:", data);
    mostrarFacturas(data);
  } catch (err) {
    console.error("Error al cargar facturas", err);
  }
}

function mostrarFacturas(facturas) {
  tbody.innerHTML = "";
  facturas.forEach(f => {
    const idFactura = f.idFactura ?? f.IdFactura;
    const idCliente = f.idCliente ?? f.IdCliente;
    
    // Intentar obtener idEmpleado desde DetalleServicios, si existe
    let idEmpleado = f.idEmpleado ?? f.IdEmpleado;
    if (!idEmpleado && f.Cliente?.Autoes?.length > 0) {
      const detalleServicio = f.Cliente.Autoes[0]?.DetalleServicios[0];
      if (detalleServicio?.Servicio?.Empleadoes?.length > 0) {
        idEmpleado = detalleServicio.Servicio.Empleadoes[0].idEmpleado;
      }
    }

    const clienteNombre = clientesMap[idCliente] || `Cliente no encontrado (ID: ${idCliente ?? "desconocido"})`;
    const empleadoNombre = idEmpleado ? (empleadosMap[idEmpleado] || `Empleado no encontrado (ID: ${idEmpleado})`) : "No asignado";

    const fecha = (f.Fecha ?? f.fecha)?.split("T")[0] || "";
    const total = f.Total ?? f.total ?? 0;

    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td class="p-2 border">${idFactura}</td>
      <td class="p-2 border">${clienteNombre}</td>
      <td class="p-2 border">${empleadoNombre}</td>
      <td class="p-2 border">${fecha}</td>
      <td class="p-2 border">${total}</td>
      <td class="p-2 border space-x-2">
        <button onclick='editarFactura(${JSON.stringify(f)})' class="text-blue-600">Editar</button>
        <button onclick='eliminarFactura(${idFactura})' class="text-red-600">Eliminar</button>
      </td>
    `;
    tbody.appendChild(tr);
  });
}

form.addEventListener("submit", async (e) => {
  e.preventDefault();

  const factura = {
    idCliente: selectCliente.value,
    idEmpleado: selectEmpleado.value,
    fecha: document.getElementById("fecha").value,
    total: document.getElementById("total").value
  };

  try {
    if (editando) {
      factura.idFactura = editando.idFactura ?? editando.IdFactura;
      await fetch(`${apiBase}/actualizar`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(factura)
      });
      editando = null;
    } else {
      await fetch(`${apiBase}/crear`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(factura)
      });
    }
    form.reset();
    await obtenerFacturas();
  } catch (err) {
    console.error("Error al guardar factura", err);
  }
});

function editarFactura(factura) {
  const idFactura = factura.idFactura ?? factura.IdFactura;
  const idCliente = factura.idCliente ?? factura.IdCliente;
  let idEmpleado = factura.idEmpleado ?? factura.IdEmpleado;
  if (!idEmpleado && factura.Cliente?.Autoes?.length > 0) {
    const detalleServicio = factura.Cliente.Autoes[0]?.DetalleServicios[0];
    if (detalleServicio?.Servicio?.Empleadoes?.length > 0) {
      idEmpleado = detalleServicio.Servicio.Empleadoes[0].idEmpleado;
    }
  }
  const fecha = (factura.Fecha ?? factura.fecha)?.split("T")[0];
  const total = factura.Total ?? factura.total;

  document.getElementById("idFactura").value = idFactura;
  selectCliente.value = idCliente;
  selectEmpleado.value = idEmpleado || "";
  document.getElementById("fecha").value = fecha;
  document.getElementById("total").value = total;

  editando = factura;
}

async function eliminarFactura(id) {
  if (confirm("¿Seguro que deseas eliminar esta factura?")) {
    try {
      await fetch(`${apiBase}/eliminar?id=${id}`, { method: "DELETE" });
      await obtenerFacturas();
    } catch (err) {
      console.error("Error al eliminar factura", err);
    }
  }
}

async function iniciar() {
  await cargarClientes();
  await cargarEmpleados();
  await obtenerFacturas();
}

iniciar();
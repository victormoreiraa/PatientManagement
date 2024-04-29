<template>
    <div id="app">
        <div class="container">
            <h1 class="bg-light p-4 mb-4 text-center display-4 mt-4">Gerenciamento de Pacientes</h1>
            <button class="btn btn-primary mb-4" @click="abrirModal">Novo Paciente</button>

            <div class="overlay" :class="{ 'show': modalAberto }"></div>

            <div class="modal" :class="{ 'show': modalAberto }" tabindex="-1" role="dialog" style="display: none;" ref="modal">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Novo Paciente</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="nome">Nome:</label>
                                <input type="text" class="form-control" v-model="novoPaciente.name" :class="{ 'is-invalid': submitted && !novoPaciente.name }" required>
                                <span v-if="submitted && !novoPaciente.name" class="invalid-feedback">Nome &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="idade">Idade:</label>
                                <input type="number" class="form-control" v-model="novoPaciente.age" :class="{ 'is-invalid': submitted && !novoPaciente.age }" required>
                                <span v-if="submitted && !novoPaciente.age" class="invalid-feedback">Idade &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="sexo">Sexo:</label>
                                <select class="form-control" v-model="novoPaciente.sex" :class="{ 'is-invalid': submitted && !novoPaciente.sex }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMININO">FEMININO</option>
                                </select>
                                <span v-if="submitted && !novoPaciente.sex" class="invalid-feedback">Sexo &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="Exam1">Exame 1:</label>
                                <input type="number" id="exam1" class="form-control" v-model="novoPaciente.exam1" :class="{ 'is-invalid': submitted && !novoPaciente.exam1 }" required>
                                <span v-if="submitted && !novoPaciente.exam1" class="invalid-feedback">Exame 1 &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="Exam2">Exame 2:</label>
                                <select class="form-control" v-model="novoPaciente.exam2" :class="{ 'is-invalid': submitted && !novoPaciente.exam2 }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="CRIAN&Ccedil;A">CRIAN&Ccedil;A</option>
                                    <option value="ADOLESCENTE">ADOLESCENTE</option>
                                    <option value="HOMEM ADULTO">HOMEM ADULTO</option>
                                    <option value="MULHER ADULTA">MULHER ADULTA</option>
                                </select>
                                <span v-if="submitted && !novoPaciente.exam2" class="invalid-feedback">Exame 2 &eacute; obrigat&oacute;rio</span>
                            </div>

                            <div class="form-group">
                                <label for="Exam4">Exame 4:</label>
                                <input type="number" id="exam4" class="form-control" v-model="novoPaciente.exam4" :class="{ 'is-invalid': submitted && !novoPaciente.exam4 }" required>
                                <span v-if="submitted && !novoPaciente.exam4" class="invalid-feedback">Exame 4 &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="Calculation">C&aacute;lculo:</label>
                                <input type="text" class="form-control" v-model="novoPaciente.calculation" @blur="realizarCalculo" :class="{ 'is-invalid': submitted && !novoPaciente.calculation }" required>
                                <span v-if="submitted && !novoPaciente.calculation" class="invalid-feedback">C&aacute;lculo &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="exam3">Exame 3:</label>
                                <input type="number" class="form-control" v-model="novoPaciente.exam3" :class="{ 'is-invalid': submitted && !novoPaciente.exam3 }" readonly>
                                <span v-if="submitted && !novoPaciente.exam3" class="invalid-feedback">Exame 3 &eacute; obrigat&oacute;rio</span>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" @click="adicionarNovoPaciente">Salvar</button>
                            <button type="button" class="btn btn-secondary" @click="fecharModal">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>


            <div class="overlay" :class="{ 'show': modalEditarAberto  }"></div>

            <div class="modal" :class="{ 'show': modalEditarAberto }" tabindex="-1" role="dialog" style="display: none;" ref="modalEditar">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Editar Paciente</h5>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="nome">Nome:</label>
                                <input type="text" class="form-control" v-model="pacienteEditado.name" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.name }" required>
                                <span v-if="submittedEdit && !pacienteEditado.name" class="invalid-feedback">Nome &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="idade">Idade:</label>
                                <input type="number" class="form-control" v-model="pacienteEditado.age" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.age }" required>
                                <span v-if="submittedEdit && !pacienteEditado.age" class="invalid-feedback">Idade &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="sexo">Sexo:</label>
                                <select class="form-control" v-model="pacienteEditado.sex" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.sex }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="MASCULINO">MASCULINO</option>
                                    <option value="FEMININO">FEMININO</option>
                                </select>
                                <span v-if="submittedEdit && !pacienteEditado.sex" class="invalid-feedback">Sexo &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="Exam1">Exame 1:</label>
                                <input type="number"  id="exam1edit" class="form-control" v-model="pacienteEditado.exam1" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.exam1 }" required>
                                <span v-if="submittedEdit && !pacienteEditado.exam1" class="invalid-feedback">Exame 1 &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="Exam2">Exame 2:</label>
                                <select class="form-control" v-model="pacienteEditado.exam2" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.exam2 }" required>
                                    <option value="" disabled>SELECIONE</option>
                                    <option value="CRIAN&Ccedil;A">CRIAN&Ccedil;A</option>
                                    <option value="ADOLESCENTE">ADOLESCENTE</option>
                                    <option value="HOMEM ADULTO">HOMEM ADULTO</option>
                                    <option value="MULHER ADULTA">MULHER ADULTA</option>
                                </select>
                                <span v-if="submittedEdit && !pacienteEditado.exam2" class="invalid-feedback">Exame 2 &eacute; obrigat&oacute;rio</span>
                            </div>

                            <div class="form-group">
                                <label for="Exam4">Exame 4:</label>
                                <input type="number" id="exam4edit" class="form-control" v-model="pacienteEditado.exam4" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.exam4 }" required>
                                <span v-if="submittedEdit && !pacienteEditado.exam4" class="invalid-feedback">Exame 4 &eacute; obrigat&oacute;rio</span>
                            </div>

                            <div class="form-group">
                                <label for="Calculation">C&aacute;lculo:</label>
                                <input type="text" class="form-control" v-model="pacienteEditado.calculation" @blur="realizarCalculoEdit" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.calculation }" required>
                                <span v-if="submittedEdit && !pacienteEditado.calculation" class="invalid-feedback">C&aacute;lculo &eacute; obrigat&oacute;rio</span>
                            </div>
                            <div class="form-group">
                                <label for="exam3">Exame 3:</label>
                                <input type="number" class="form-control text-uppercase" v-model="pacienteEditado.exam3" :class="{ 'is-invalid': submittedEdit && !pacienteEditado.exam3 }" readonly>
                                <span v-if="submittedEdit && !pacienteEditado.exam3" class="invalid-feedback">Exame 3 &eacute; obrigat&oacute;rio</span>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" @click="salvarEdicaoPaciente">Salvar</button>
                            <button type="button" class="btn btn-secondary" @click="fecharModalEditar">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>Idade</th>
                            <th>Sexo</th>
                            <th>Exame 1</th>
                            <th>Exame 2</th>
                            <th>Exame 3</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="paciente in pacientes" :key="paciente.id">
                            <td>{{ paciente.name }}</td>
                            <td>{{ paciente.age }}</td>
                            <td>{{ paciente.sex }}</td>
                            <td :class="getExame1Class(paciente)">{{ paciente.exam1 }}</td>
                            <td class="text-uppercase" :class="getExame2Class(paciente)">{{ paciente.exam2 }}</td>
                            <td>{{ paciente.exam3 }}</td>
                            <td>
                                <button class="btn btn-secondary" @click="editarPaciente(paciente)">Editar</button>
                                &nbsp;
                                <button class="btn btn-danger" @click="excluirPaciente(paciente)">Excluir</button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                pacientes: [],
                novoPaciente: {
                    name: '',
                    age: null,
                    sex: '',
                    exam1: null,
                    exam2: '',
                    exam3: null,
                    exam4: null,
                    calculation: ''
                },
                modalAberto: false,
                pacienteEditado: {
                    id: null,
                    name: '',
                    age: null,
                    sex: '',
                    exam1: null,
                    exam2: '',
                    exam3: null,
                    exam4: null,
                    calculation:''
                },
                modalEditarAberto: false,
                submitted: false,
                submittedEdit: false

            };
        },
        created() {
            this.carregarPacientes();
        },
        methods: {
            async carregarPacientes() {
                try {
                    const response = await fetch('http://localhost:5111/api/patient/');
                    if (!response.ok) {
                        throw new Error('Erro ao carregar pacientes');
                    }
                    const data = await response.json();
                    this.pacientes = data;
                } catch (error) {
                    console.error('Erro ao carregar pacientes:', error);
                }
            },
            async editarPaciente(paciente) {
                this.pacienteEditado = { ...paciente };
                this.modalEditarAberto = true; 
                document.body.classList.add('modal-open');
                this.$refs.modalEditar.style.display = 'block'; 
            },

            async salvarEdicaoPaciente() {

                this.submittedEdit = true;

                if (!this.pacienteEditado.name || !this.pacienteEditado.age || !this.pacienteEditado.sex) {
                    return; 
                }
                if (this.pacienteEditado.exam2) {
                    this.pacienteEditado.exam2 = this.pacienteEditado.exam2.toUpperCase();
                }
                try {
                    const response = await fetch(`http://localhost:5111/api/patient/${this.pacienteEditado.id}`, {
                        method: 'PUT',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.pacienteEditado)
                    });

                    if (!response.ok) {
                        throw new Error('Erro ao salvar edição do paciente');
                    }

                    console.log("Paciente editado com sucesso!");
                    this.fecharModalEditar();
                    this.carregarPacientes();
                } catch (error) {
                    console.error('Erro ao salvar edição do paciente:', error);
                }
            },

            fecharModalEditar() {
                this.modalEditarAberto = false;
                document.body.classList.remove('modal-open');
                this.$refs.modalEditar.style.display = 'none'; 
            },
            async excluirPaciente(paciente) {
                try {
                    const response = await fetch(`http://localhost:5111/api/patient/${paciente.id}`, {
                        method: 'DELETE'
                    });
                    if (!response.ok) {
                        throw new Error('Erro ao excluir paciente');
                    }
                    console.log("Paciente excluído com sucesso!");
                    this.carregarPacientes();
                } catch (error) {
                    console.error('Erro ao excluir paciente:', error);
                }
            },
            realizarCalculo() {
                const exam1 = this.novoPaciente.exam1;
                const exam4 = this.novoPaciente.exam4;

                const calculo = this.novoPaciente.calculation;

                if (calculo && calculo.trim() !== '') {
                    try {
                        let expressao = calculo.replace(/exame1/g, exam1).replace(/exame4/g, exam4);

                        expressao = expressao.replace(/raiz_quadrada/g, 'Math.sqrt');

                        expressao = expressao.replace(/\^/g, '**');
                        expressao = expressao.replace(/x/gi, '*');
                        


                        let resultado = eval(expressao);
                        resultado = parseFloat(resultado.toFixed(2));
                        if (!isNaN(resultado)) {
                            this.novoPaciente.exam3 = resultado;
                        } else {
                            console.error('Resultado inválido:', resultado);
                        }
                    } catch (error) {
                        console.error('Erro ao calcular:', error);
                    }
                }
            },
            realizarCalculoEdit() {
                const exam1 = this.pacienteEditado.exam1;
                const exam4 = this.pacienteEditado.exam4;

                const calculo = this.pacienteEditado.calculation;

                if (calculo && calculo.trim() !== '') {
                    try {
                        let expressao = calculo.replace(/exame1/g, exam1).replace(/exame4/g, exam4);

                        expressao = expressao.replace(/raiz_quadrada/g, 'Math.sqrt');

                        expressao = expressao.replace(/\^/g, '**');
                        expressao = expressao.replace(/x/gi, '*');

                        let resultado = eval(expressao);
                        resultado = parseFloat(resultado.toFixed(2));
                        if (!isNaN(resultado)) {
                            this.pacienteEditado.exam3 = resultado;
                        } else {
                            console.error('Resultado inválido:', resultado);
                        }
                    } catch (error) {
                        console.error('Erro ao calcular:', error);
                    }
                }
            },
            async adicionarNovoPaciente() {
                this.submitted = true; 
               
                if (!this.novoPaciente.name || !this.novoPaciente.age || !this.novoPaciente.sex) {
                    return; 
                }
                try {
                    if (!this.novoPaciente) {
                        throw new Error('Objeto novoPaciente não foi inicializado corretamente.');
                    }
                    if (this.novoPaciente.exam2) {
                        this.novoPaciente.exam2 = this.novoPaciente.exam2.toUpperCase();
                    }
                    const response = await fetch('http://localhost:5111/api/patient/', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.novoPaciente)
                    });

                    if (!response.ok) {
                        throw new Error('Erro ao adicionar novo paciente');
                    }

                    console.log("Novo paciente adicionado com sucesso!");
                    this.fecharModal();
                    this.carregarPacientes();
                } catch (error) {
                    console.error('Erro ao adicionar novo paciente:', error);
                }
            },
            getExame1Class(paciente) {
                if (paciente.age < 10 && paciente.exam1 >= 5 && paciente.exam1 <= 10) {
                    return 'table-success';
                } else if (paciente.sex === 'MASCULINO' && paciente.age >= 10 && paciente.exam1 >= 10 && paciente.exam1 <= 15 ) {
                    return 'table-success';
                } else if (paciente.sex === 'FEMININO' && paciente.age >= 10 && paciente.exam1 >= 10 && paciente.exam1 <= 20) {
                    return 'table-success';
                } else {
                    return 'table-danger';
                }
            },
            getExame1Text(paciente) {
                if (paciente.age < 10 && paciente.exam1 >= 5 && paciente.exam1 <= 10) {
                    return 'POSITIVO';
                } else if (paciente.sex === 'MASCULINO' && paciente.age >= 10 && paciente.exam1 >= 10 && paciente.exam1 <= 15) {
                    return 'POSITIVO';
                } else if (paciente.sex === 'FEMININO' && paciente.age >= 10 && paciente.exam1 >= 10 && paciente.exam1 <= 20) {
                    return 'POSITIVO';
                } else {
                    return 'NEGATIVO';
                }
            },
            getExame2Class(paciente) {
               if (paciente.age < 12 && paciente.exam2 == "CRIANÇA") {
                    return 'table-success';
                } else if (paciente.age >= 12 && paciente.age < 18 && paciente.exam2 == "ADOLESCENTE" ) {
                    return 'table-success';
                } else if (paciente.sex === 'MASCULINO' && paciente.age >= 18 && paciente.exam2 == "HOMEM ADULTO") {
                    return 'table-success';
                } else if (paciente.sex === 'FEMININO' && paciente.age >= 18 && paciente.exam2 == "MULHER ADULTA") {
                    return 'table-success';
                } else {
                    return 'table-danger';
                }
            },
            getExame2Text(paciente) {
                if (paciente.age < 12 && paciente.exam2 == "CRIANÇA") {
                    return 'POSITIVO';
                } else if (paciente.age >= 12 && paciente.age <= 18 == "ADOLESCENTE") {
                    return 'POSITIVO';
                } else if (paciente.sex === 'MASCULINO' && paciente.age >= 18 && paciente.exam2 == "HOMEM ADULTO") {
                    return 'POSITIVO';
                } else if (paciente.sex === 'FEMININO' && paciente.age >= 18 && paciente.exam2 == "MULHER ADULTA") {
                    return 'POSITIVO';
                } else {
                    return 'NEGATIVO';
                }
            },
            abrirModal() {
                this.modalAberto = true;
                document.body.classList.add('modal-open');
                this.$refs.modal.style.display = 'block';
            },
            fecharModal() {
                this.modalAberto = false;
                document.body.classList.remove('modal-open');
                this.$refs.modal.style.display = 'none';
            }

        }
    };
</script>

<style>

    .overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5); 
        z-index: 999; 
        display: none;
    }

        .overlay.show {
            display: block; 
        }

    .modal.show {
        display: block;
    }

    .table-success {
        background-color: #d4edda;
    }

    .table-danger {
        background-color: #f8d7da;
    }

    
</style>

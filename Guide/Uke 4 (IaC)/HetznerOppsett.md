TaskService.infrastructure/
│
├── main.tf
├── variables.tf
├── outputs.tf
├── cloud-init.yaml
└── .auto.tfvars

+.gitignore (i todo-prosjekt)

1. Opprett infrastructure-mappen

Lag:

- main.tf
- variables.tf

2. Sett opp Terraform provider

I main.tf:

- terraform {}
- required_providers
- provider "hcloud"

3. Lag variables.tf

Definer:

- hcloud_token
- ssh_key_name

Eventuelt:

- server_name
- server_type
- location
- image

4. Lag .auto.tfvars fil

Her legger vi hemmelige eller lokale verdier:

hcloud_token = "..."
ssh_key_name = "..."

.auto.tfvars lastes inn automatisk og skal IKKE pushes til GitHub.

5. Lag .gitignore

_.tfstate
_.tfstate.backup
.terraform.lock.hcl
_.tfvars
_.auto.tfvars
.terraform/

6. Lag cloud-init

---

TaskService.infrastructure/
│
├── main.tf
├── variables.tf
├── .auto.tfvars
│
└── modules/
└── app-server/
├── main.tf
├── variables.tf
├── outputs.tf
└── cloud-init.yaml

1. Lag modules/app-server

Inneholder:

- main.tf
- variables.tf
- outputs.tf
- cloud-init.yaml

2. Flytt server resource fra root/main.tf til app-server/main.tf

3. Lag en cloud-init.yaml

Installer:

- git
- docker compose
- docker
- eventuelt mer

---

1. Kjør Terraform

terraform init
terraform apply

2. Sjekk IP-adressen

ssh root@SERVER_IP

Hvis cloud-init ikke kjører, sjekk med:
cloud-init status

ELLER

sudo cat /var/log/cloud-init-output.log

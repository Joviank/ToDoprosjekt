terraform {
  required_providers {
    hcloud = {
      source  = "hetznercloud/hcloud"
      version = "1.69.0"
    }
  }
}

resource "hcloud_server" "server" {
    name = "To-doListe"
    image = "ubuntu-24.04"
    server_type = "cpx12"
    location = "hel1"

    ssh_keys = [var.ssh_key_name]

    user_data = file("${path.module}/cloud-init.yaml")
}